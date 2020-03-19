using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class DoorStateChangedEventArgs : EventArgs
    {
        public bool IsClosed { get; set; }
    }
}
