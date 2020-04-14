using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
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
       
        
        [SetUp]
        public void Setup()
        {
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
        
        [TestCase(-1.0)]
        [TestCase(0.0)]
        [TestCase(0.1)]
        [TestCase(2)]
        [TestCase(4.9)]
        [TestCase(5.0)]
        [TestCase(5.1)]
        [TestCase(250)]
        [TestCase(499)]
        [TestCase(500)]
        [TestCase(501)]
        public void NewCurrentEvent_CurrentIsCorrectValue_(double current)
        {
           _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            Assert.That(_uut.CurrentValue, Is.EqualTo(current));
        }


        /*Tilføjet til genaflevering*/

        [Test]
        public void BehaviourTest_IfCurrentTooLow_DisplayNoConnection()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = 0});
            _display.Received(1).DisplayMessage("Ingen forbindelse til telefon. Opladning ikke startet.");
        }

        [TestCase(0.1)]
        [TestCase(2)]
        [TestCase(4.9)]
        [TestCase(5.0)]
        public void BehaviourTest_IfCurrentBetweenZeroAndFive_DisplayFullyCharged(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            _display.Received(1).DisplayMessage("Telefonen er fuldt opladet");
        }

        
        [TestCase(5.1)]
        [TestCase(250)]
        [TestCase(499)]
        [TestCase(500)]
        public void BehaviourTest_IfCurrentNormal_DisplayChargingStarted(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            _display.Received(1).DisplayMessage("Opladning er i gang.");
        }


        [TestCase(501)]
        public void BehaviourTest_IfCurrentTooHigh_StopChargeCalled(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            _usbCharger.Received(1).StopCharge();
        }

        /*stop*/



        [Test]
        public void StopCharge_()
        {
            _uut.StopCharge();
            _usbCharger.Received().StopCharge();
        }

        [Test]
        public void StartCharge_()
        {
            _uut.StartCharge();
            _usbCharger.Received().StartCharge();
        }

    }
}
