using System;

namespace Ladeskab
{
    public interface IChargeControl
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;
        
        // Direct access to the current current value
        double CurrentValue { get; }

        // Require connection status of the phone
        bool IsConnected { get; }

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
    }
}