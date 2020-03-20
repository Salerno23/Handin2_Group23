using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using NUnit.Framework;

namespace Handin2.Test.Unit
{
    [TestFixture]
    public class DoorUnitTest
    {
        private Door _uut;
        private DoorStateChangedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;

            _uut = new Door();

            //eventlistener
            _uut.DoorStateChangedEvent += (o, args) =>
            {
                _receivedEventArgs = args;
            };
        }

        [Test]
        public void setDoorStateToNewState_TestEventFired()
        {
            _uut.SetDoorState(true);
            Assert.That(_receivedEventArgs, Is.Not.Null);
            
        }

        [TestCase(true)]
        [TestCase(false)]
        public void setDoorStateToNewState_TestStateIsSetCorrect(bool state)
        {
            _uut.SetDoorState(state);
            Assert.That(_receivedEventArgs.IsClosed, Is.EqualTo(state));
        }


        

    }
}
