using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handin2.RFID;

namespace Ladeskab
{
    class RFIDReader : IRFIDReader
    {
        public event EventHandler<ReadRFIDEventArgs> ReadRFIDEvent;

        public void SetRFIDTag(int rfidTag)
        {
            OnReadRFID(new ReadRFIDEventArgs{RFIDTag = rfidTag});
        }

        protected  virtual void OnReadRFID(ReadRFIDEventArgs e)
        {
            ReadRFIDEvent?.Invoke(this, e);
        }
    }
}
