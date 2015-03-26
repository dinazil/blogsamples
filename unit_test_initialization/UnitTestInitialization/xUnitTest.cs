using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestInitialization.xUnitUtils;
using Xunit;

namespace UnitTestInitialization
{
    [PrioritizedFixture]
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
        [TestPriority(1)]
        public void Test1()
        {
            Assert.Equal(1, _instanceCounter);
        }

        [Fact, TestPriority(2)]
        public void Test2()
        {
            Assert.Equal(2, _instanceCounter);
        }

        [Fact, TestPriority(3)]
        public void Test3()
        {
            Assert.Equal(3, _instanceCounter);
        }

        [Fact, TestPriority(4)]
        public void StaticTest4()
        {
            Assert.Equal(4, _classCounter);
        }

        [Fact, TestPriority(5)]
        public void StaticTest5()
        {
            Assert.Equal(5, _classCounter);
        }

        [Fact, TestPriority(6)]
        public void StaticTest6()
        {
            Assert.Equal(6, _classCounter);
        }
    }
}
