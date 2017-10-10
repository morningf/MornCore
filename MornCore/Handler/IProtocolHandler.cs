using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    public interface IProtocolHandler
    {
        string Process(MornRequestData data);
    }
}
