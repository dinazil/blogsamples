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
        private static int _classCounter = 0;
        
        [SetUp]
        public void Setup()
        {
            ++_instanceCounter;
            ++_classCounter;
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

        [Test]
        public void TestStatic4()
        {
            Assert.AreEqual(4, _classCounter);
        }

        [Test]
        public void TestStatic5()
        {
            Assert.AreEqual(5, _classCounter);
        }

        [Test]
        public void TestStatic6()
        {
            Assert.AreEqual(6, _classCounter);
        }
    }
}
