using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    /// <summary>
    /// 数据验证接口
    /// </summary>
    public interface IProtocolDataVerifiable
    {
        /// <summary>
        /// 是否验证AppKey是否存在
        /// </summary>
        bool VerifyAppKey { get; set; }
        /// <summary>
        /// 是否验证签名是否正确
        /// </summary>
        bool VerifySignature { get; set; }
        /// <summary>
        /// 是否验证AppKey对此API是否有权限访问
        /// </summary>
        bool VerifyAuthority { get; set; }
        /// <summary>
        /// 验证MornRequestData的数据是否符合要求，当不符合要求时，应该抛出MornException
        /// </summary>
        /// <param name="data"></param>
        void Validate(MornRequestData data);
    }
}
