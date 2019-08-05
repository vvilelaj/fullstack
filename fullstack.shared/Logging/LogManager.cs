using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.shared.Logging
{
    public class LogManager : ILogManager
    {
        private const string INFO = "INFO";
        private const string ERROR = "ERROR";
        private readonly string _application;

        public void AddInfo(string className, string method, string args, string message, long elapsedTime)
        {
            try
            {
                LogEvent log = new LogEvent(INFO, className, method, args, message, _application, elapsedTime, null);

                // todo : implemennt save information in kibana, file, server
            }
            catch (Exception e)
            {
                // todo : implemennt save information in kibana, file, server
            }
        }

        public void AddError(string className, string method, string args, string message, long elapsedTime, Exception ex)
        {
            try
            {
                LogEvent log = new LogEvent(ERROR, className, method, args, message, _application, elapsedTime, JsonConvert.SerializeObject(ex));

                // todo : implemennt save information in kibana, file, server
            }
            catch (Exception e)
            {
                // todo : implemennt save information in kibana, file, server
            }
        }

        public void AddInfo(string message)
        {
            try
            {
                LogEvent log = new LogEvent(INFO, null, null, null, message, _application, 0, null);

                // todo : implemennt save information in kibana, file, server
            }
            catch (Exception e)
            {

                // todo : implemennt save information in kibana, file, server
            }
        }
    }
}