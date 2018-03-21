using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore.Text
{
    class StringUtil
    {
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
