using Microsoft.VisualStudio.TestTools.UnitTesting;
using YTP.Main.Areas.SportsStore.Controllers;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace YTP.MainTest.SportsStore {

    [TestClass]
    public class UnitTest1 {

        [TestMethod]
        public void CanPaginate() {

            //arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, ProductName = "P1"},
                new Product {ProductID = 2, ProductName = "P2"},
                new Product {ProductID = 3, ProductName = "P3"},
                new Product {ProductID = 4, ProductName = "P4"},
                new Product {ProductID = 5, ProductName = "P5"},
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            IEnumerable<Product> result = (IEnumerable<Product>)controller.ListProducts(2); //

            //Assert
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].ProductName, "P4");
            Assert.AreEqual(prodArray[1].ProductName, "P5");
        }
    }
}
