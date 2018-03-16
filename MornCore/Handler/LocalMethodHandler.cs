using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    public class LocalMethodHandler<TRequest, TResponse> : IProtocolHandler
        where TRequest : MornRequest<TResponse>
        where TResponse : MornResponse
    {
        public LocalMethodHandler(Func<TRequest, IServiceProvider, TResponse> localFunc)
        {
            _localFunc = localFunc ?? throw new MornException(MornErrorType.MornException, "LocalMethodHandler的构造不能使用空参数");
        }

        Func<TRequest, IServiceProvider, TResponse> _localFunc;

        public string Process(MornRequestData data)
        {
            var request = JsonConvert.DeserializeObject<TRequest>(data.Data);
            var response = _localFunc.Invoke(request, data.RequestServices);
            return JsonConvert.SerializeObject(response);
        }
    }
}
