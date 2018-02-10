using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MornCore.FastLog
{
    /// <summary>
    /// 日志内容
    /// </summary>
    class FastLogMessage
    {
        public DateTime LogTime { get; set; }
        public string LogTag { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
