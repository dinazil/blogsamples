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
        
        [TestInitialize]
        public void Setup()
        {
            ++_instanceCounter;
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
    }
}
