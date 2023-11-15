using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using YTP.Main.Areas.SandboxTBP.Models;

namespace YTP.MainTest {
    /// <summary>
    /// Summary description for EssentialToolsTest
    /// </summary>
    [TestClass]
    public class EssentialToolsTest {

        private IDiscountHelper getTestObject() {
            return new MinimumDiscountHelper();
        }

        [TestMethod]
        public void Above_100() {
            //arrange
            IDiscountHelper target = getTestObject();
            decimal total = 200;

            //act
            var discountedTotal = target.ApplyDiscount(total);

            //assert
            Assert.AreEqual(total * 0.9M, discountedTotal);
        }

        [TestMethod]
        public void Between10And100() {
            //arrange
            IDiscountHelper target = getTestObject();

            //act
            decimal tenDollarDiscount = target.ApplyDiscount(10);
            decimal HundredDollarDiscount = target.ApplyDiscount(100);
            decimal FiftyDollarDiscount = target.ApplyDiscount(50);

            //assert
            Assert.AreEqual(5, tenDollarDiscount, "$10 discount is wrong");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 discount is wrong");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 discount is wrong");
        }

        [TestMethod]
        public void LessThan10() {
            //arrange
            IDiscountHelper target = getTestObject();            

            //act
            decimal discount5 = target.ApplyDiscount(5);
            decimal discount0 = target.ApplyDiscount(0);

            //assert
            Assert.AreEqual(5,discount5);
            Assert.AreEqual(0, discount0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NegativeTotal() {
            //arrange
            IDiscountHelper target = getTestObject();

            //act
            target.ApplyDiscount(-1);
        }
    }
}
