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
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private LadeskabState _state;
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private IChargeControl _charger;
        private IDoor _door;
        private IDisplay _display;

        private int _oldId;

        public bool DoorState { get; set; }
        public int ReadRFIDTag { get; set; }

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor doorStatus, IRFIDReader rfidReader, IDisplay display, IChargeControl charger)
        {
            doorStatus.DoorStateChangedEvent += HandleDoorStateChangedEvent;
            rfidReader.ReadRFIDEvent += HandleReadRFIDEvent;

            _door = doorStatus;
            _display = display;
            _charger = charger;

            _state = LadeskabState.Available;
        }


        // Her mangler de andre trigger handlere
        private void HandleDoorStateChangedEvent(object sender, DoorStateChangedEventArgs e)
        {
            DoorState = e.IsClosed;
        }

        private void HandleReadRFIDEvent(object sender, ReadRFIDEventArgs e)
        {
            ReadRFIDTag = e.RFIDTag;
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = ReadRFIDTag;

                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", e.RFIDTag);
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
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
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
