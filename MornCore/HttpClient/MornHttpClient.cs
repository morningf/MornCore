using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MornCore
{
    class MornHttpClient
    {
        private int _timeout = 100000;
        private int _readWriteTimeout = 300000;

        /// <summary>
        /// 请求超时前等待的毫秒数。 默认值是 100,000 毫秒（100 秒）。
        /// </summary>
        public int Timeout
        {
            get { return this._timeout; }
            set { this._timeout = value; }
        }

        /// <summary>
        /// 在写入超时或读取超时之前的毫秒数。 默认值为 300,000 毫秒（5 分钟）。
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return this._readWriteTimeout; }
            set { this._readWriteTimeout = value; }
        }

        public string DoPost(string url, string data)
        {
            var request = GetWebRequest(url);

            byte[] postData = Encoding.UTF8.GetBytes(data);
            using (var stream = request.GetRequestStream())
            {
                stream.Write(postData, 0, postData.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string result = string.Empty;         
            using (var stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                }
            }
            response.Close();
            return result;
        }

        public async Task<string> DoPostAsync(string url, string data) => await Task.Factory.StartNew(() => { return DoPost(url, data); });

        HttpWebRequest GetWebRequest(string url)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                request = (HttpWebRequest)HttpWebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);
            }

            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            request.Timeout = _timeout;
            request.ReadWriteTimeout = _readWriteTimeout;

            return request;
        }
    }
}
