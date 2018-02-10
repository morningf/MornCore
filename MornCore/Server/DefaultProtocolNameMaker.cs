using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    class DefaultProtocolNameMaker : IProtocolGiveName
    {
        public string GiveName(Type type)
        {
            string name = type.Name;
            //以下的处理去掉了协议的最后的结尾符Request
            return Text.StringUtil.GetLowerSplitString(name.Remove(name.Length - 7), ".");
        }
    }
}
