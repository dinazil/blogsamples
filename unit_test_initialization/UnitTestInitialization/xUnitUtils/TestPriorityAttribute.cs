using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestInitialization.xUnitUtils
{
    // taken from http://www.bricelam.net/2012/04/xunitnet-extensibility.html 
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    class TestPriorityAttribute : Attribute
    {
        readonly int _priority;

        public TestPriorityAttribute(int priority)
        {
            _priority = priority;
        }

        public int Priority
        {
            get { return _priority; }
        }
    }
}
