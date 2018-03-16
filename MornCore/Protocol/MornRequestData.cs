using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MornCore
{
    public class MornRequestData
    {
        private MornRequestData()
        {
        }

        private Dictionary<string, string> _textParams = new Dictionary<string, string>();

        public string AppKey
        {
            get
            {
                string value;
                if (_textParams.TryGetValue(MornConstants.APP_KEY, out value))
                    return value;
                else
                    return string.Empty;
            }
        }
        public string Sign
        {
            get
            {
                string value;
                if (_textParams.TryGetValue(MornConstants.SIGN, out value))
                    return value;
                else
                    return string.Empty;
            }
        }
        public string Method
        {
            get
            {
                string value;
                if (_textParams.TryGetValue(MornConstants.METHOD, out value))
                    return value;
                else
                    return string.Empty;
            }
        }
        public string TimeStamp
        {
            get
            {
                string value;
                if (_textParams.TryGetValue(MornConstants.TIMESTAMP, out value))
                    return value;
                else
                    return string.Empty;
            }
        }
        public string Data
        {
            get
            {
                string value;
                if (_textParams.TryGetValue(MornConstants.DATA, out value))
                    return value;
                else
                    return string.Empty;
            }
        }
        /// <summary>
        /// 原始内容
        /// </summary>
        public string Context { get; private set; }
        public IServiceProvider RequestServices { get; private set; }

        public IDictionary<string, string> GetTextParamsWithoutSign()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>(_textParams);
            dic.Remove(MornConstants.SIGN);
            return dic;
        }

        public static MornRequestData Create(HttpContext context)
        {
            MornRequestData data = new MornRequestData();
            data.RequestServices = context.RequestServices;
            var request = context.Request;

            //准备内容元素
            using (StreamReader sr = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                data.Context = sr.ReadToEnd();
            }
            var map = JsonConvert.DeserializeObject<IDictionary<string, string>>(data.Context);
            foreach (var kv in map)
            {
                data._textParams[kv.Key] = kv.Value;
            }

            return data;
        }
    }
}
