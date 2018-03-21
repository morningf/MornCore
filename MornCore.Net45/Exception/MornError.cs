using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    public enum MornErrorType
    {
        NoError = 0,

        #region 1000-3999
        DefaultNetError = 1000,
        SystemError,
        InvalidAppKey,
        InvalidParameter,
        InvalidAuthority,
        SignError,
        UnknownMethod,
        #endregion

        MornException = 4000,
    }

    public static class MornErrorUtil
    {
        private static Dictionary<MornErrorType, string> _errors;

        static MornErrorUtil()
        {
            _errors = new Dictionary<MornErrorType, string>()
            {
                { MornErrorType.NoError, "" },

                { MornErrorType.DefaultNetError, "未定义的错误" },
                { MornErrorType.SystemError, "系统异常" },
                { MornErrorType.InvalidAppKey, "AppKey无效" },
                { MornErrorType.InvalidParameter, "参数不合法" },
                { MornErrorType.InvalidAuthority, "访问被拒绝" },
                { MornErrorType.SignError, "签名错误" },
                { MornErrorType.UnknownMethod, "不存在的方法" },

                { MornErrorType.MornException, "本地异常" },
            };
        }

        public static string GetErrorCode(MornErrorType type)
        {
            return ((int)type).ToString();
        }

        public static string GetDescription(MornErrorType type)
        {
            string r;
            return _errors.TryGetValue(type, out r) ? r : "未知错误";
        }

        /// <summary>
        /// 生成错误回应字符串
        /// </summary>
        /// <param name="type">错误类型</param>
        /// <returns>错误回应字符串</returns>
        public static string GenError(MornErrorType type)
        {
            return JsonConvert.SerializeObject(new Dictionary<string, object>()
                {
                    { MornConstants.ERROR_CODE, MornErrorUtil.GetErrorCode(type) },
                    { MornConstants.ERROR_MESSAGE, MornErrorUtil.GetDescription(type) },
                });
        }

        public static string GenError(MornErrorType type, string addition)
        {
            if (string.IsNullOrEmpty(addition))
            {
                return GenError(type);
            }
            else
            {
                return JsonConvert.SerializeObject(new Dictionary<string, object>()
                    {
                        { MornConstants.ERROR_CODE, GetErrorCode(type) },
                        { MornConstants.ERROR_MESSAGE, string.Format("{0} : {1}", GetDescription(type), addition) },
                    });
            }
        }
    }
}

