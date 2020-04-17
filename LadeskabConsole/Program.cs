using System;
using Ladeskab;

namespace Ladeskab
{
    class Program
    {
      
        static void Main(string[] args)
        {
            Door door = new Door();
            RFIDReader rfidReader = new RFIDReader();
            Display display = new Display();
            UsbChargerSimulator usbChargerSimulator = new UsbChargerSimulator();
            ChargeControl chargeControl = new ChargeControl(usbChargerSimulator, display);
            Logging logging = new Logging();

            StationControl stationControl = new StationControl(door, rfidReader, display, chargeControl, logging);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R, A: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.SetDoorState(true);
                        break;

                    case 'C':
                        door.SetDoorState(false);
                        
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.SetRFIDTag(id);
                        break;

                    case 'A':
                        System.Console.WriteLine("Tilslut telefon Y/N");
                        string conString = System.Console.ReadLine();
                        
                        if(conString == "Y" )
                        {
                            usbChargerSimulator.SimulateConnected(true);
                        }
                        else
                        {
                            usbChargerSimulator.SimulateConnected(false);
                        }
                        
                        break;


                    default:
                        break;
                }

            } while (!finish);
        }
    }
}

