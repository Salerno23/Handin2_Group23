using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class ReadRFIDEventArgs : EventArgs
    {
        public int RFIDTag { get; set; }
    }
}
