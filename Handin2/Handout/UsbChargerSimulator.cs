using System;
using System.Timers;

namespace Ladeskab
{
    public class UsbChargerSimulator : IChargeControl
    {
        // Constants
        private const double MaxCurrent = 500.0; // mA
        private const double FullyChargedCurrent = 2.5; // mA
        private const double OverloadCurrent = 750; // mA
        private const int ChargeTimeMinutes = 20; // minutes
        private const int CurrentTickInterval = 250; // ms

        public event EventHandler<CurrentEventArgs> CurrentValueEvent;

        public double CurrentValue { get; private set; }

        public bool IsConnected { get; private set; }

        private bool _overload;
        private bool _charging;
        private System.Timers.Timer _timer;
        private int _ticksSinceStart;

        public UsbChargerSimulator()
        {
            CurrentValue = 0.0;
            IsConnected = true;
            _overload = false;

            _timer = new System.Timers.Timer();
            _timer.Enabled = false;
            _timer.Interval = CurrentTickInterval;
            _timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            // Only execute if charging
            if (_charging)
            {
                _ticksSinceStart++;
                if (IsConnected && !_overload)
                {
                    double newValue = MaxCurrent - 
                                      _ticksSinceStart * (MaxCurrent - FullyChargedCurrent) / (ChargeTimeMinutes * 60 * 1000 / CurrentTickInterval);
                    CurrentValue = Math.Max(newValue, FullyChargedCurrent);
                }
                else if (IsConnected && _overload)
                {
                    CurrentValue = OverloadCurrent;
                }
                else if (!IsConnected)
                {
                    CurrentValue = 0.0;
                }

                OnNewCurrent();
            }
        }

        public void SimulateConnected(bool connected)
        {
            IsConnected = connected;
        }

        public void SimulateOverload(bool overload)
        {
            _overload = overload;
        }

        public void StartCharge()
        {
            // Ignore if already charging
            if (!_charging)
            {
                if (IsConnected && !_overload)
                {
                    CurrentValue = 500;
                }
                else if (IsConnected && _overload)
                {
                    CurrentValue = OverloadCurrent;
                }
                else if (!IsConnected)
                {
                    CurrentValue = 0.0;
                }

                OnNewCurrent();
                _ticksSinceStart = 0;

                _charging = true;

                _timer.Start();
            }
        }

        public void StopCharge()
        {
            _timer.Stop();

            CurrentValue = 0.0;
            OnNewCurrent();

            _charging = false;
        }

        private void OnNewCurrent()
        {
            CurrentValueEvent?.Invoke(this, new CurrentEventArgs() {Current = this.CurrentValue});
        }
    }
}
