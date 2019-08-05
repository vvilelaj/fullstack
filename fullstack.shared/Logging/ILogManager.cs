using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullstack.shared.Logging
{
    public interface ILogManager
    {
        void AddError(string className, string method, string args, string message, long elapsedTime, Exception ex);
        void AddInfo(string className, string method, string args, string message, long elapsedTime);
        void AddInfo(string message);
    }
}
