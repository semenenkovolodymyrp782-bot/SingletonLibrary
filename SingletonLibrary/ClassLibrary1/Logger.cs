using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ClassLibrary1
{
    public class Logger
    {
        private static Logger instance;

        private Logger()
        {
        }

        public static Logger getInstance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }

            return instance;
        }

        public void Log(string message)
        {
            LogEntry entry = new LogEntry(message);

            string path = "log.txt";

            File.AppendAllText(
                path,
                entry.ToString() + Environment.NewLine
            );
        }
    }
}
