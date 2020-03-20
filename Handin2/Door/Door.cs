using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Door : IDoor
    {
        public bool _IsLocked { get; set; }
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
            if(state == true)
            {
                OnDoorOpen(new DoorStateChangedEventArgs { IsClosed = state });
                _oldDoorState = state;
            }
            else if (state == false)
            {
                OnDoorClose(new DoorStateChangedEventArgs { IsClosed = state });
                _oldDoorState = state;
            }
        }

        //protected virtual void OnDoorStateChanged(DoorStateChangedEventArgs e)
        //{
        //    DoorStateChangedEvent?.Invoke(this, e);
        //}

        protected virtual void OnDoorOpen(DoorStateChangedEventArgs e)
        {
            DoorStateChangedEvent?.Invoke(this, e);
        }

        protected virtual void OnDoorClose(DoorStateChangedEventArgs e)
        {
            DoorStateChangedEvent?.Invoke(this, e);
        }

    }
}
