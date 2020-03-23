using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Handin2.Test.Unit
{
    public class ChargeControlUnitTest
    {

        private ChargeControl _uut;
        private IUsbCharger _usbCharger;
        private IDisplay _display;
        private CurrentEventArgs _receivedCurrentEventArgs;
        private ConnectedEventArgs _receivedConnectedEventArgs;
        
        [SetUp]
        public void Setup()
        {
            _receivedCurrentEventArgs = null;
            _receivedConnectedEventArgs = null;
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_usbCharger, _display);

        }

        
       
    }
}
