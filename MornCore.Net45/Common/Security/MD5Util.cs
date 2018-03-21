using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MornCore.Security
{
    public static class MD5Util
    {
        public static string StrToMD5(string str)
        {
            byte[] data = Encoding.Default.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < OutBytes.Length; i++)
            {
                sb.Append(OutBytes[i].ToString("x2"));
            }
            return sb.ToString().ToUpper();
        }

        public static string BytesToMd5(byte[] arr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(arr);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < OutBytes.Length; i++)
            {
                sb.Append(OutBytes[i].ToString("x2"));
            }
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 计算某文件的MD5，文件不会被全部加在到内存
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMD5ByHashAlgorithm(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new ArgumentException(string.Format("<{0}>, 不存在", path));
            int bufferSize = 1024 * 16;//自定义缓冲区大小16K  
            byte[] buffer = new byte[bufferSize];
            System.IO.Stream inputStream = System.IO.File.Open(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
            int readLength = 0;//每次读取长度  
            var output = new byte[bufferSize];
            while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                //计算MD5  
                hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
            }
            //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)  
            hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
            string md5 = BitConverter.ToString(hashAlgorithm.Hash);
            hashAlgorithm.Clear();
            inputStream.Close();
            md5 = md5.Replace("-", "");
            return md5;
        }
    }
}
