using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Logging : ILogging
    {
        private string logFile = "logfile.txt";

        public void Log(int RFIDTag, string logText)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + logText + "{0}", RFIDTag);
            }
        }
    }
}
