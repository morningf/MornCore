using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    public static class MornServer
    {
        /// <summary>
        /// 数据验证接口，通过设置此接口可以为基本数据设置不同的数据验证规则
        /// </summary>
        public static IProtocolDataVerifiable ProtocolDataVerifier
        {
            get => MornService.ProtocolDataVerifier;
            set => MornService.ProtocolDataVerifier = value;
        }
        /// <summary>
        /// 是否验证AppKey是否存在
        /// </summary>
        public static bool VerifyAppKey
        {
            get
            {
                return MornService.ProtocolDataVerifier == null ?
                    false : MornService.ProtocolDataVerifier.VerifyAppKey;
            }
            set
            {
                if (MornService.ProtocolDataVerifier == null)
                {
                    throw new MornException(MornErrorType.MornException);
                }
                else
                {
                    MornService.ProtocolDataVerifier.VerifyAppKey = value;
                }
            }
        }
        /// <summary>
        /// 是否验证签名是否正确
        /// </summary>
        public static bool VerifySignature
        {
            get
            {
                return MornService.ProtocolDataVerifier == null ?
                    false : MornService.ProtocolDataVerifier.VerifySignature;
            }
            set
            {
                if (MornService.ProtocolDataVerifier == null)
                {
                    throw new MornException(MornErrorType.MornException);
                }
                else
                {
                    MornService.ProtocolDataVerifier.VerifySignature = value;
                }
            }
        }
        /// <summary>
        /// 是否验证AppKey对此API是否有权限访问
        /// </summary>
        public static bool VerifyAuthority
        {
            get
            {
                return MornService.ProtocolDataVerifier == null ?
                    false : MornService.ProtocolDataVerifier.VerifyAuthority;
            }
            set
            {
                if (MornService.ProtocolDataVerifier == null)
                {
                    throw new MornException(MornErrorType.MornException);
                }
                else
                {
                    MornService.ProtocolDataVerifier.VerifyAuthority = value;
                }
            }
        }
        /// <summary>
        /// 已经注册的方法名
        /// </summary>
        public static IEnumerable<string> MethodNameCollection
        {
            get
            {
                return MornService.ProtocolHandlerNameCollection;
            }
        }

        /// <summary>
        /// 注册远程处理接口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        public static void RegisterRemoteMethodHandler(string name, string url)
        {
            MornService.RegisterProtocolHandler(name, new RemoteMethodHandler(url));
        }
        /// <summary>
        /// 注册远程处理接口
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        public static void RegisterRemoteMethodHandler<TRequest, TResponse>(string url)
        {
            var name = MornService.ProtocolNameMaker.GiveName(typeof(TRequest));
            RegisterRemoteMethodHandler(name, url);
        }
        /// <summary>
        /// 注册本地处理接口
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="handler"></param>
        public static void RegisterLocalMethodHandler<TRequest, TResponse>(LocalMethodHandler<TRequest, TResponse> handler) 
            where TRequest : MornRequest<TResponse>
            where TResponse : MornResponse
        {
            var name = MornService.ProtocolNameMaker.GiveName(typeof(TRequest));
            RegisterProtocolHandler(name, handler);
        }
        /// <summary>
        /// 注册本地处理接口
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="localFunc"></param>
        public static void RegisterLocalMethodHandler<TRequest, TResponse>(Func<TRequest, IServiceProvider, TResponse> localFunc)
            where TRequest : MornRequest<TResponse>
            where TResponse : MornResponse
        {
            var handler = new LocalMethodHandler<TRequest, TResponse>(localFunc);
            RegisterLocalMethodHandler(handler);
        }
        /// <summary>
        /// 注册协议处理接口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="handler"></param>
        public static void RegisterProtocolHandler(string name, IProtocolHandler handler)
        {
            MornService.RegisterProtocolHandler(name, handler);
        }
    }
}
