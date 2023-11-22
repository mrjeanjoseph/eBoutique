using Microsoft.VisualStudio.TestTools.UnitTesting;
using YTP.Main.Areas.SportsStore.Controllers;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;
using System.Collections.Generic;
using Moq;
using System.Web.Mvc;
using System.Linq;

namespace YTP.MainTest.SportsStore {
    [TestClass]
    public class AdminTests {
        [TestMethod]
        public void IndexContainsAllProducts() {

            //Arrange - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, ProductName = "Product name One"},
                new Product { ProductID = 2, ProductName = "Product name Two"},
                new Product { ProductID = 3, ProductName = "Product name Three"},
                new Product { ProductID = 4, ProductName = "Product name Four"},
                new Product { ProductID = 5, ProductName = "Product name Five"}
            });

            //Arrage - Create a controller
            AdminController target = new AdminController(mock.Object);

            //Action
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            //Assert
            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual("Product name One", result[0].ProductName);
            Assert.AreEqual("Product name Two", result[1].ProductName);
            Assert.AreEqual("Product name Three", result[2].ProductName);
        }
    }
}
