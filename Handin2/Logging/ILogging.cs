using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public interface ILogging
    {
        void Log(int RFIDTag, string logText);
    }
}
