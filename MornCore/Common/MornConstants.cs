using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    class MornConstants
    {
        #region 作为Key的字符串
        public static readonly string APP_KEY = "AppKey";
        public static readonly string SIGN = "Sign";
        public static readonly string METHOD = "Method";
        public static readonly string TIMESTAMP = "Timestamp";
        public static readonly string DATA = "Data";
        public static readonly string ERROR_CODE = "ErrCode";
        public static readonly string ERROR_MESSAGE = "ErrMsg";
        #endregion

        #region 格式化字符串
        public static readonly string DATE_FORMAT = "yyyy-MM-dd";
        public static readonly string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public static readonly string DATE_TIME_MS_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";
        #endregion
    }
}
