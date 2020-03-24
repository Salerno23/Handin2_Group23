using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        [Test]
        public void NewConnectedEvent_EventFired_Connected()
        {
            _usbCharger.ConnectedEvent += Raise.EventWith(new ConnectedEventArgs(){Connected = true});
            Assert.That(_uut.IsConnected, Is.Not.Null);
        }
        
        
        [Test]
        public void NewCurrentEvent_EventFired_Current()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = 200});
            Assert.That(_uut.CurrentValue, Is.Not.Null);
        }
        
        
    }
}
