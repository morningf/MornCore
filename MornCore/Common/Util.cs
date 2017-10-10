using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MornCore
{
    class Util
    {
        /// <summary>
        /// 给请求签名。
        /// </summary>
        /// <param name="parameters">所有字符型的请求参数</param>
        /// <param name="secret">签名密钥</param>
        /// <returns>签名</returns>
        public static string SignRequestByMd5Method(IDictionary<string, string> parameters, string secret)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder(secret);
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
                }
            }
            query.Append(secret);

            // 第三步：使用MD5加密
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));

            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }

            return result.ToString();
        }

        /// <summary>
        /// 将大小写字符串分割并插入分隔符，如TheTest默认返回为the_test
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public static string GetLowerSplitString(string str, string split = "_")
        {
            if (string.IsNullOrEmpty(str))
                return "";

            StringBuilder r = new StringBuilder();
            r.Append(Char.ToLower(str[0]));
            for (int i = 1; i < str.Length; ++i)
            {
                char c = str[i];
                if (Char.IsUpper(c))
                {
                    if (i == str.Length - 1 || Char.IsLower(str[i + 1]))
                        r.Append(split);
                    r.Append(Char.ToLower(c));
                }
                else
                {
                    r.Append(c);
                }
            }

            return r.ToString();
        }
    }
}
