using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MornCore.FastLog
{
    public class FastLog
    {
        /// <summary>
        /// 日志队列
        /// </summary>
        private readonly ConcurrentQueue<FastLogMessage> _queue;
        /// <summary>
        /// 信号
        /// </summary>
        private readonly ManualResetEvent _mre;
        /// <summary>
        /// 写日志流
        /// </summary>
        private StreamWriter _sw;

        private FastLog(string path, string nameFormat = "fastlog_{0}.log")
        {
            _queue = new ConcurrentQueue<FastLogMessage>();
            _mre = new ManualResetEvent(false);
            string name = string.Format(nameFormat, DateTime.Now.ToString("yyyy_MM_dd"));
            _sw = new StreamWriter(System.IO.Path.Combine(path, name), true);

            Thread t = new Thread(new ThreadStart(WriteLog));
            t.IsBackground = false;
            t.Start();
        }

        static FastLog _instance;
        public static FastLog Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("fastlog not registered");
                }
                return _instance;
            }
        }

        private static bool _isRegisted = false;
        public static void Register(string path)
        {
            if (_instance != null)
                return;

            _instance = new FastLog(path);
            _isRegisted = true;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志文本</param>
        /// <param name="level">等级</param>
        /// <param name="ex">Exception</param>
        private void EnqueueMessage(string message, string tag, Exception ex = null)
        {
            _queue.Enqueue(new FastLogMessage
            {
                LogTime = DateTime.Now,
                LogTag = tag,
                Message = message,
                Exception = ex
            });

            // 通知线程往磁盘中写日志
            _mre.Set();
        }
        public static void Log(string msg, string tag, Exception ex = null)
        {
            if (_isRegisted)
            {
                Instance.EnqueueMessage(msg, tag, ex);
            }
        }
        public static void Log(string msg, Exception ex = null)
        {
            Log(msg, null, ex);
        }

        void WriteLog()
        {
            while (true)
            {
                // 等待信号通知
                _mre.WaitOne();

                FastLogMessage msg;
                // 判断是否有内容需要如磁盘 从列队中获取内容，并删除列队中的内容
                while (_queue.TryDequeue(out msg))
                {
                    if (_sw != null)
                    {
                        _sw.WriteLine("+log [{0}] {1}\r\n{2}\r\n{3}",
                            msg.LogTime.ToString("yyyy-MM-dd HH:mm:ss,fff"),
                            string.IsNullOrEmpty(msg.LogTag) ? "" : "#" + msg.LogTag + "#",
                            msg.Message,
                            GenExceptionMessage(msg.Exception));
                        _sw.Flush();
                    }
                }

                // 重新设置信号
                _mre.Reset();
                Thread.Sleep(1);
            }
        }

        string GenExceptionMessage(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("--{0}----------------------------------\r\n", ex.GetType().Name);
            sb.AppendLine(ex.Message);
            if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
            {
                sb.AppendLine("--InnerException-----------------------------");
                sb.AppendLine(ex.InnerException.Message);
            }
            if (!string.IsNullOrEmpty(ex.StackTrace))
            {
                sb.AppendLine("--StackTrace-----------------------------");
                sb.AppendLine(ex.StackTrace);
            }
            return sb.ToString();
        }
    }
}
