using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    /// <summary>
    /// 适用于小型项目的默认数据验证器
    /// </summary>
    public class DefaultProtocolDataValidator : IProtocolDataVerifiable
    {
        public DefaultProtocolDataValidator()
        {
            VerifyAppKey = true;
            VerifySignature = true;
            VerifyAuthority = true;
        }
        public bool VerifyAppKey { get; set; }
        public bool VerifySignature { get; set; }
        public bool VerifyAuthority { get; set; }

        public class AppItem
        {
            public AppItem(string appKey)
            {
                AppKey = appKey;
            }
            public string AppKey { get; private set; }
            public string AppSecret { get; set; }
            public HashSet<string> AuthoritySet { get; set; }
        }

        private Dictionary<string, AppItem> _appMap = new Dictionary<string, AppItem>();
        public void Add(AppItem item)
        {
            _appMap[item.AppKey] = item;
        }

        public void Validate(MornRequestData data)
        {
            if (!VerifyAppKey && !VerifySignature && !VerifyAuthority)
                return;

            AppItem item;
            if (!_appMap.TryGetValue(data.AppKey, out item))
            {
                throw new MornException(MornErrorType.InvalidAppKey);
            }

            if (VerifySignature)
            {
                if (string.IsNullOrEmpty(item.AppSecret))
                    throw new MornException(MornErrorType.SignError);
                var dic = data.GetTextParamsWithoutSign();
                var sign = Security.Signature.SignRequestByMd5Method(dic, item.AppSecret);
                if (!data.Sign.Equals(sign))
                    throw new MornException(MornErrorType.SignError);
            }

            if (VerifyAuthority)
            {
                if (item.AuthoritySet == null)
                    throw new MornException(MornErrorType.InvalidAuthority);
                if (!item.AuthoritySet.Contains(data.Method))
                    throw new MornException(MornErrorType.InvalidAuthority);
            }
        }
    }
}
