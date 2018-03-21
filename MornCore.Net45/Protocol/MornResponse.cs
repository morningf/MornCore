using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    public abstract class MornResponse
    {
        private string _errorCode = "0";
        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }
        public string ErrorMessage { get; set; }
    }
}
