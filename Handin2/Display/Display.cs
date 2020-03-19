using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    class Display : IDisplay
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
