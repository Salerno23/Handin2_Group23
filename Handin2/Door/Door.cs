using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Door : IDoor
    {
        private bool _IsLocked;
        private bool _oldDoorState;
        public event EventHandler<DoorStateChangedEventArgs> DoorStateChangedEvent;


        public void LockDoor()
        {
            _IsLocked = true;
        }

        public void UnlockDoor()
        {
            _IsLocked = false;
        }

        public void SetDoorState(bool state)
        {
            if(state != _oldDoorState)
            {
                OnDoorStateChanged(new DoorStateChangedEventArgs { IsClosed = state });
                _oldDoorState = state;
            }
        }

        protected virtual void OnDoorStateChanged(DoorStateChangedEventArgs e)
        {
            DoorStateChangedEvent?.Invoke(this, e);
        }

    }
}
