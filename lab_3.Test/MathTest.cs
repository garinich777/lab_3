using System;
using lab_3.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab_3.Test
{
    [TestClass]
    public class MathTest
    {
        [TestMethod]
        public void TestFuncX8_A16_EV6_928()
        {
            double x = 8;
            double a = 16;
            double ExpectedValue = 6.928;

            Assert.AreEqual(ExpectedValue, Math.Round(MathModel.GetFunc(x, a), 3));
        }

        [TestMethod]
        public void TestFuncX0_A16_EV0()
        {
            double x = 0;
            double a = 16;
            double ExpectedValue = 0;

            Assert.AreEqual(ExpectedValue, Math.Round(MathModel.GetFunc(x, a), 3));
        }

        [TestMethod]
        public void TestFuncX20_A16_EVNaN()
        {
            double x = 20;
            double a = 16;
            double ExpectedValue = double.NaN;

            Assert.AreEqual(ExpectedValue, Math.Round(MathModel.GetFunc(x, a), 3));
        }

        [TestMethod]
        public void TestFuncX16_A16_EV0()
        {
            double x = 16;
            double a = 16;
            double ExpectedValue = 0;

            Assert.AreEqual(ExpectedValue, Math.Round(MathModel.GetFunc(x, a), 3));
        }
    }
}
