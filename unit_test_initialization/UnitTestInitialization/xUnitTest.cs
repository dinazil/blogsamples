using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestInitialization
{
    public class xUnitTest
    {
        private int _instanceCounter = 0;
        private static int _classCounter = 0;

        public xUnitTest()
        {
            ++_instanceCounter;
            ++_classCounter;
        }

        [Fact]
        public void Test1()
        {
            Assert.Equal(1, _instanceCounter);
        }

        [Fact]
        public void Test2()
        {
            Assert.Equal(2, _instanceCounter);
        }

        [Fact]
        public void Test3()
        {
            Assert.Equal(3, _instanceCounter);
        }

        [Fact]
        public void StaticTest4()
        {
            Assert.Equal(4, _classCounter);
        }

        [Fact]
        public void StaticTest5()
        {
            Assert.Equal(5, _classCounter);
        }

        [Fact]
        public void StaticTest6()
        {
            Assert.Equal(6, _classCounter);
        }
    }
}
