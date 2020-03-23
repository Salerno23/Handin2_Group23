using System;

namespace Ladeskab
{
    public class CurrentEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Current { set; get; }
    }

    public class ConnectedEventArgs : EventArgs
    {
        public bool Connected { set; get; }
    }

    public interface IUsbCharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;

        //Event trigger when phone is connected
        event EventHandler<ConnectedEventArgs> ConnectedEvent;

        // Direct access to the current current value
        double CurrentValue { get; }

        // Require connection status of the phone
        bool Connected { get; }

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
    }
}