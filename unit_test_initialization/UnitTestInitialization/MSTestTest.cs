using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestInitialization
{
    [TestClass]
    public class MSTestTest
    {
        private int _instanceCounter = 0;
        private static int _classCounter = 0;
        
        [TestInitialize]
        public void Setup()
        {
            ++_instanceCounter;
            ++_classCounter;
        }

        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(1, _instanceCounter);
        }

        [TestMethod]
        public void Test2()
        {
            Assert.AreEqual(2, _instanceCounter);
        }

        [TestMethod]
        public void Test3()
        {
            Assert.AreEqual(3, _instanceCounter);
        }

        [TestMethod]
        public void StaticTest4()
        {
            Assert.AreEqual(4, _classCounter);
        }

        [TestMethod]
        public void StaticTest5()
        {
            Assert.AreEqual(5, _classCounter);
        }

        [TestMethod]
        public void StaticTest6()
        {
            Assert.AreEqual(6, _classCounter);
        }
    }
}
