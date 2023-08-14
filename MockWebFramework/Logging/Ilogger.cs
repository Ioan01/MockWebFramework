using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Logging
{

    enum MessageType
    {
        Info,
        Warning,
        Error,
        Fatal
    }
    internal interface Ilogger
    {
        void LogInfo(string message);

        void LogWarning(string message);

        void LogError(string message);

        void LogFatal(string message);

        public static Ilogger Instance => new BasicLogger();

    }
}
