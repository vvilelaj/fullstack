using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.shared.Logging
{
    public class LogEvent
    {
        public DateTime Date { get; set; }
        public string HostName { get; set; }
        public int ThreadId { get; set; }
        public string Level { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
        public string Parameters { get; set; }
        public string Message { get; set; }
        public string Application { get; set; }
        public long ElapsedTime { get; set; }
        public string Exception { get; set; }

        public LogEvent(string level, string className, string method, string parameters,
            string message, string application, long elapsedTime, string exception)
        {
            this.Date = DateTime.Now;
            this.HostName = System.Net.Dns.GetHostName();
            this.ThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

            this.Level = level;
            this.Class = className;
            this.Method = method;
            this.Parameters = parameters;
            this.Message = message;
            this.Application = application;
            this.ElapsedTime = elapsedTime;
            this.Exception = exception;
        }
    }
}