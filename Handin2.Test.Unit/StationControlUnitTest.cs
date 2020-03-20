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

        [Test]
        public void DoorStateChanged_EventFired_DoorStateChanged()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs() { IsClosed = false });
            Assert.That(_uut.DoorState, Is.False);
        }

        [TestCase(10)]
        [TestCase(-1)]
        public void ReadRFID_DifferentArguments_CurrentRFIDIsCorrect(int tag)
        {
            _rfidReader.ReadRFIDEvent += Raise.EventWith(new ReadRFIDEventArgs{RFIDTag = tag});
            Assert.That(_uut.ReadRFIDTag,Is.EqualTo(tag));
        }
    }
}
