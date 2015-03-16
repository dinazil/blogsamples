using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestInitialization
{
    [TestFixture]
    class NUnitTest
    {
        private int _instanceCounter = 0;
        
        [SetUp]
        public void Setup()
        {
            ++_instanceCounter;
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(1, _instanceCounter);
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(2, _instanceCounter);
        }

        [Test]
        public void Test3()
        {
            Assert.AreEqual(3, _instanceCounter);
        }
    }
}
