using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Door : IDoor
    {
        public event EventHandler<DoorStateChangedEventArgs> DoorStateChangedEvent;


        public void LockDoor()
        {

        }

        public void UnlockDoor()
        {

        }

        protected virtual void OnDoorStateChanged(DoorStateChangedEventArgs e)
        {
            DoorStateChangedEvent?.Invoke(this, e);
        }

    }
}
