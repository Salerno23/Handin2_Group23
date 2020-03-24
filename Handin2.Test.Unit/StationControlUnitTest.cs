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
    [TestFixture]
    public class StationControlUnitTest
    {
        private StationControl _uut;

        private IDoor _door;
        private IRFIDReader _rfidReader;
        private IDisplay _display;
        private IChargeControl _charger;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRFIDReader>();
            _display = Substitute.For<IDisplay>();
            _charger = Substitute.For<IChargeControl>();

            _uut = new StationControl(_door, _rfidReader, _display, _charger);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void DoorStateChanged_EventFired_DoorStateChanged(bool state)
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs() { IsClosed = state});
            Assert.That(_uut.DoorState, Is.EqualTo(state));
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void ReadRFID_DifferentArguments_CurrentRFIDIsCorrect(int tag)
        {
            _rfidReader.ReadRFIDEvent += Raise.EventWith(new ReadRFIDEventArgs{RFIDTag = tag});
            Assert.That(_uut.ReadRFIDTag,Is.EqualTo(tag));
        }

        [Test]
        public void ReadRFID_LockDoorCalled_InAvailable()
        {
            _charger.IsConnected.Returns(true);
            _rfidReader.ReadRFIDEvent += Raise.EventWith(new ReadRFIDEventArgs() {RFIDTag = 32});
            _door.Received(1).LockDoor();
        }

        [Test]
        public void ReadRFID_StartChargeCalled_InAvailable()
        {
            _charger.IsConnected.Returns(true);
            _rfidReader.ReadRFIDEvent += Raise.EventWith(new ReadRFIDEventArgs() { RFIDTag = 32 });
            _charger.Received(1).StartCharge();
        }

        [Test]
        public void ReadRFID_DisplayMessageCalled_InAvailable()
        {
            _charger.IsConnected.Returns(true);
            _rfidReader.ReadRFIDEvent += Raise.EventWith(new ReadRFIDEventArgs() { RFIDTag = 32 });
            _display.Received(1).DisplayMessage("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        [Test]
        public void DoorOpened_Test()
        {
            _door.SetDoorState(true);
        }
    }
}
