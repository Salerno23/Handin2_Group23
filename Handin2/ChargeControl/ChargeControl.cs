using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class ChargeControl: IChargeControl 
    {
       
        public event EventHandler<CurrentEventArgs> CurrentValueEvent;
        public event EventHandler<ConnectedEventArgs> ConnectedEvent;

        private readonly IDisplay _display;
        private readonly IUsbCharger _usbCharger;
        public double CurrentValue { get; private set; }
        public bool IsConnected { get; private set; }

        public ChargeControl( IUsbCharger usbCharger, IDisplay display)
        {
            usbCharger.CurrentValueEvent += HandleNewCurrentEvent;
            usbCharger.ConnectedEvent += HandleNewConnectedEvent;
            _usbCharger = usbCharger;
            _display = display;
        }

        private void HandleNewCurrentEvent(object s, CurrentEventArgs e)
        {
            CurrentValue = e.Current;
            if (CurrentValue == 0.0)
            {
                _display.DisplayMessage("Ingen forbindelse til telefon. Opladning ikke startet.");
            }
            else if (0.0 < CurrentValue && CurrentValue <= 5.0)
            {
                _display.DisplayMessage("Telefonen er fuldt opladet");
            }
            else if ((5 < CurrentValue) && (CurrentValue <= 500.0))
            {
                _display.DisplayMessage("Opladning er i gang.");
            }
            else if (CurrentValue > 500.0)
            {
                _display.DisplayMessage("Der er sket en fejl. Frakobl venligst telefonen");
            }
        }


        private void HandleNewConnectedEvent(object s, ConnectedEventArgs e)
        {
            IsConnected = e.Connected;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
            _display.DisplayMessage("telefon oplader");
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
            _display.DisplayMessage("Telefon har stoppet opladning");
        }

    }
}
