using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Logging
{
    internal class BasicLogger : Ilogger
    {
        private MessageType _lasMessageType = MessageType.Info;

        private string format(string type, string message) => $"{type} {DateTime.Now.ToString("HH:mm:ss")} {message}";

        public void LogInfo(string message)
        {
            if (_lasMessageType != MessageType.Info)
                Console.ResetColor();
            Console.WriteLine(format("INFO",message));
        }

        public void LogWarning(string message)
        {
            if (_lasMessageType != MessageType.Warning)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine(format("WARNING", message));
        }

        public void LogError(string message)
        {
            if (_lasMessageType != MessageType.Error)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(format("ERROR", message));
        }

        public void LogFatal(string message)
        {
            if (_lasMessageType != MessageType.Fatal)
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }

            Console.WriteLine(format("FATAL", message));
        }

        public static BasicLogger Logger { get; private set; } = new BasicLogger();



    }
}
