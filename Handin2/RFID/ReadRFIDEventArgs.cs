using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handin2.RFID
{
    public class ReadRFIDEventArgs : EventArgs
    {
        public int RFIDTag { get; set; }
    }
}
