﻿using System;
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

            StationControl stationControl = new StationControl(door, rfidReader, display, chargeControl);



            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
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



                    default:
                        break;
                }

            } while (!finish);
        }
    }
}

