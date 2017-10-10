using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    /// <summary>
    /// 协议Request数据结构，每一个Request应该对应一个Response。
    /// 在命名时应该遵循Request和Response结尾的规则。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MornRequest<T> where T : MornResponse
    {
        /// <summary>
        /// 请求的API名称。
        /// </summary>
        public virtual string GetApiName()
        {
            return MornService.ProtocolNameMaker.GiveName(GetType());
        }
        /// <summary>
        /// 获取序列化后的json字符串
        /// </summary>
        /// <returns></returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// 客户端参数检查，减少服务端无效调用。
        /// </summary>
        public virtual void Validate()
        {

        }
    }
}
