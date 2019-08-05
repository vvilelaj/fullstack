using fullstack.shared.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace fullstack.shared.Controllers
{
    public class BaseApiController : ApiController
    { 
        protected static ILogManager LogManager { get;}
        static BaseApiController()
        {
            LogManager = new LogManager();
        }

        protected static Stopwatch GetStopWatch()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            return stopWatch;
        }
        
    }
}