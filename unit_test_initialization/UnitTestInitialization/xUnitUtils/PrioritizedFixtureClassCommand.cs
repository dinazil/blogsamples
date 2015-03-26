using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit.Sdk;

namespace UnitTestInitialization.xUnitUtils
{
    // taken from http://www.bricelam.net/2012/04/xunitnet-extensibility.html 
    class PrioritizedFixtureClassCommand : ITestClassCommand
    {
        // The default implementation.
        // Assume members not shown simply delegate to this
        readonly TestClassCommand _inner = new TestClassCommand();

        public int ChooseNextTest(ICollection<IMethodInfo> testsLeftToRun)
        {
            return 0;
        }

        public IEnumerable<IMethodInfo> EnumerateTestMethods()
        {
            return from m in _inner.EnumerateTestMethods()
                   let priority = GetPriority(m)
                   orderby priority
                   select m;
        }

        private static int GetPriority(IMethodInfo method)
        {
            var priorityAttribute = method
                .GetCustomAttributes(typeof(TestPriorityAttribute))
                .FirstOrDefault();

            return priorityAttribute == null
                ? 0
                : priorityAttribute.GetPropertyValue<int>("Priority");
        }


        public Exception ClassFinish()
        {
            return _inner.ClassFinish();
        }

        public Exception ClassStart()
        {
            return _inner.ClassStart();
        }

        public IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo testMethod)
        {
            return _inner.EnumerateTestCommands(testMethod);
        }

        public bool IsTestMethod(IMethodInfo testMethod)
        {
            return _inner.IsTestMethod(testMethod);
        }

        public object ObjectUnderTest
        {
            get { return _inner.ObjectUnderTest; }
        }

        public ITypeInfo TypeUnderTest
        {
            get
            {
                return _inner.TypeUnderTest;
            }
            set
            {
                _inner.TypeUnderTest = value;
            }
        }
    }
}
