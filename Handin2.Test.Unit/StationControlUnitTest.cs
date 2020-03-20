using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handin2.RFID;
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

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRFIDReader>();

            _uut = new StationControl(_door, _rfidReader);

        }

        [Test]
        public void DoorStateChanged_EventFired_DoorStateChanged()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs() { IsClosed = false });
            Assert.That(_uut.DoorState, Is.False);
        }
    }
}
