using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestRunner(1, 3, 3);
        }

        [TestMethod]
        public void TestMethod2()
        {
            TestRunner(2, 4, 6);
        }

        public void TestRunner(int a, int b, int c)
        {
            //Act
            c -= b;

            //Assert
            Assert.AreEqual(a, c);
            Assert.IsTrue(b > c);
        }
    }
}
