using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MornCore
{
    static class MornService
    {
        static ConcurrentDictionary<string, IProtocolHandler> _protocolHandlerMap = new ConcurrentDictionary<string, IProtocolHandler>();

        public static IProtocolDataVerifiable ProtocolDataVerifier { get; set; }

        public static IEnumerable<string> ProtocolHandlerNameCollection
        {
            get
            {
                return _protocolHandlerMap.Keys;
            }
        }

        public static async Task ProcessHttpContextAsync(HttpContext context)
        {
            string responseString = string.Empty;
            try
            {
                responseString = GetResponseString(context);
            }
            catch (Exception ex)
            {
                responseString = MornErrorUtil.GenError(
                    MornErrorType.SystemError,
                    ex.Message);
            }

            context.Response.ContentType = "application/json;charset=UTF-8";
            await context.Response.WriteAsync(responseString);
        }

        public static void RegisterProtocolHandler(string name, IProtocolHandler handler)
        {
            if (!_protocolHandlerMap.TryAdd(name, handler))
            {
                throw new MornException(MornErrorType.MornException, "已经注册过同名的协议处理接口");
            }
        }

        static string GetResponseString(HttpContext context)
        {
            MornRequestData requestData = MornRequestData.Create(context);

            if (ProtocolDataVerifier != null)
            {
                try
                {
                    ProtocolDataVerifier.Validate(requestData);
                }
                catch (MornException ex)
                {
                    return MornErrorUtil.GenError(
                        ex.ErrorType,
                        ex.Message);
                }
            }

            var handler = GetProtocolHandler(requestData.Method);
            if (handler == null)
            {
                return MornErrorUtil.GenError(MornErrorType.UnknownMethod);
            }

            return handler.Process(requestData);
        }

        static IProtocolHandler GetProtocolHandler(string name)
        {
            IProtocolHandler handler = null;
            return _protocolHandlerMap.TryGetValue(name, out handler) ? handler : null;
        }
    }
}
