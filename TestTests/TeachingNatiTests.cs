using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test;
using System;
using System.Collections.Generic;
using System.Text;
using Functions.Extensions;

namespace Test.Tests
{
    [TestClass()]
    public class TeachingNatiTests
    {
        [TestMethod()]
        public void MainTest()
        {
            string[] res = { "first", "second", "third forth", "actual fifth" };
            string[] a = {"first", "second"}, b = {"third forth", "actual fifth"};
            
            Assert.AreEqual(res.AllElementsToString(), a.JoinTogether(b).AllElementsToString());
        }
    }
}