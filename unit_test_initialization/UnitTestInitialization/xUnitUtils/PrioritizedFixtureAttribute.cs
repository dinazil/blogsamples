using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestInitialization.xUnitUtils
{
    // taken from http://www.bricelam.net/2012/04/xunitnet-extensibility.html 
    class PrioritizedFixtureAttribute : RunWithAttribute
    {
        public PrioritizedFixtureAttribute()
            : base(typeof(PrioritizedFixtureClassCommand))
        {
        }
    }
}
