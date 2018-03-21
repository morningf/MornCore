using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MornCore
{
    class MornException : Exception
    {
        public MornErrorType ErrorType { get; private set; }
        public string ErrorCode
        {
            get
            {
                return MornErrorUtil.GetErrorCode(ErrorType);
            }
        }

        public MornException(MornErrorType type)
            : base()
        {
            ErrorType = type;
        }
        public MornException(MornErrorType type, string message)
            : base(message)
        {
            ErrorType = type;
        }       
    }
}
