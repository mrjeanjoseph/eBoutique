using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using YTP.Main.Areas.SandboxTBP.Models;

namespace YTP.MainTest {
    /// <summary>
    /// Summary description for ShoppingCartTest
    /// </summary>
    [TestClass]
    public class ShoppingCartTest {

        private readonly ETProduct[] products = {
            new ETProduct { ProductName = "Bef Gras", Category = "Elvaj", ProductPrice = 2100M},
            new ETProduct { ProductName = "Bouk Kabrit", Category = "Elvaj", ProductPrice = 516M},
            new ETProduct { ProductName = "Cheval Noir", Category = "Elvaj", ProductPrice = 2850M},
            new ETProduct { ProductName = "Bari Pistash", Category = "Rekot", ProductPrice = 1150M},
            new ETProduct { ProductName = "Ke Palmis", Category = "Natif", ProductPrice = 472M}
        };


        [TestMethod]
        public void SumProductsCorrectly() {

            //arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);

            //var discounter = new MinimumDiscountHelper();
            //var target = new LinqValueCalculator(discounter);
            //var goalTotal = products.Sum(x => x.ProductPrice);

            //act
            var result = target.ValueProducts(products);

            //assert
            //Assert.AreEqual(goalTotal, result);
            Assert.AreEqual(products.Sum(e => e.ProductPrice), result); 

        }

        private ETProduct[] CreateProduct(decimal value) {
            return new[] { new ETProduct { ProductPrice = value } };

        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discount() {
            //arrage
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).Throws<System.ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => (total * 0.9M));
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10,100,Range.Inclusive))).Returns<decimal>(total => (total -5));

            var target = new LinqValueCalculator(mock.Object);

            //act
            decimal FiveDollarDiscount = target.ValueProducts(CreateProduct(5));
            decimal TenDollarDiscount = target.ValueProducts(CreateProduct(10));
            decimal FiftyDollarDiscount = target.ValueProducts(CreateProduct(50));
            decimal HundredDollarDiscount = target.ValueProducts(CreateProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProducts(CreateProduct(500));

            //assert
            Assert.AreEqual(5, FiveDollarDiscount, "$5 Fail");
            Assert.AreEqual(5, TenDollarDiscount, "$10 Fail");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 Fail");
            Assert.AreEqual(59, HundredDollarDiscount, "$100 Fail");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "$500 Fail");
            target.ValueProducts(CreateProduct(0));
        }
    }
}
