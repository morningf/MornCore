using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore.Server
{
    class DefaultProtocolDataValidator : IProtocolDataVerifiable
    {
        public bool VerifyAppKey { get; set; }
        public bool VerifySignature { get; set; }
        public bool VerifyAuthority { get; set; }

        public void Validate(MornRequestData data)
        {
            throw new NotImplementedException();
        }
    }
}
