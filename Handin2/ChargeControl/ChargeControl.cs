using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    class ChargeControl : IChargeControl
    {
        public event EventHandler<CurrentEventArgs> CurrentValueEvent;
        public double CurrentValue { get; }
        public bool IsConnected { get; }
        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }
    }
}
