using System;
using lab_3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab_3.Test
{
    [TestClass]
    public class MathTest
    {
        [TestMethod]
        public void TestFunc()
        {
            double x = 8;
            double a = 16;
            double ExpectedValue = 6.928;

            Assert.AreEqual(ExpectedValue, Math.Round(MathModel.GetFunc(x, a), 3));
        }
    }
}
