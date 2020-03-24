using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class StationControl
    {
        private LadeskabState _state;
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private readonly IChargeControl _charger;
        private readonly IDoor _door;
        private readonly IDisplay _display;

        private int _oldId;

        public bool DoorState { get; set; }
        public int ReadRFIDTag { get; set; }

        private string logFile = "logfile.txt";

        public StationControl(IDoor doorStatus, IRFIDReader rfidReader, IDisplay display, IChargeControl charger)
        {
            doorStatus.DoorStateChangedEvent += HandleDoorStateChangedEvent;
            rfidReader.ReadRFIDEvent += HandleReadRFIDEvent;

            _door = doorStatus;
            _display = display;
            _charger = charger;


            _door.UnlockDoor();
            _display.DisplayMessage("Indlæs RFID");
            _state = LadeskabState.Available;
        }

        private void HandleDoorStateChangedEvent(object sender, DoorStateChangedEventArgs e)
        {
            DoorState = e.IsClosed;

            if (DoorState)
            {
                _display.DisplayMessage("Tilslut telefon");
            }
            else
            {
                _display.DisplayMessage("Indlæs RFID");
                _state = LadeskabState.Available;
            }
        }

        private void HandleReadRFIDEvent(object sender, ReadRFIDEventArgs e)
        {
            ReadRFIDTag = e.RFIDTag;
            State();
        }

        private void State()
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    if (_charger.IsConnected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = ReadRFIDTag;

                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", ReadRFIDTag);
                        }

                        _display.DisplayMessage("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.DisplayMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    //ignore

                    break;

                case LadeskabState.Locked:
                    if (ReadRFIDTag == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();

                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", ReadRFIDTag);
                        }

                        _display.DisplayMessage("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.DisplayMessage("Forkert RFID tag");
                    }

                    break;
            }
        }
    }
}
