﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab;
using NUnit.Framework;

namespace Handin2.Test.Unit
{
    [TestFixture]
    public class RFIDUnitTest
    {
        private RFIDReader _uut;
        private ReadRFIDEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;

            _uut = new RFIDReader();
            _uut.SetRFIDTag(12);

            _uut.ReadRFIDEvent += (o, args) => { _receivedEventArgs = args; };
        }

        [Test]
        public void SetRFID_RFIDSetToNewValue_EventFired()
        {
            _uut.SetRFIDTag(13);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void SetRFID_RFIDSetToNewValue_CorrectRFIDReceived(int newTag)
        {
            _uut.SetRFIDTag(newTag);
            Assert.That(_receivedEventArgs.RFIDTag, Is.EqualTo(newTag));
        }
    }
}
