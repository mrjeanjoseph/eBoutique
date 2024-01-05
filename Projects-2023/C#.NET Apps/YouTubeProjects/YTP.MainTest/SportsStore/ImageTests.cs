using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;
using YTP.Main.Areas.SportsStore.Controllers;

namespace YTP.MainTest.SportsStore {
    [TestClass]
    public class ImageTests {

        [TestMethod]
        public void CanRetriveValidImageData() {
            //Arrange - Create a product with image data
            Product prodImage = new Product {
                ProductID = 2,
                ProductName = "Product Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            //Arrange - Create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository> ();
            mock.Setup(m => m.Products  ).Returns(new Product[] { 
                new Product { ProductID = 1, ProductName = "Product Test1"},
                prodImage,
                new Product { ProductID = 3, ProductName = "Product Test3"},
            }.AsQueryable());

            //Arrange - Create the controller
            ProductController target = new ProductController(mock.Object);

            //Act - Call the GetImage action method
            ActionResult result = target.GetImage(2);

            //Assert - 
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(prodImage.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void CannotRetriveInvalidImageData() {
            //Arrange - Create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, ProductName = "Product One"},
                new Product {ProductID = 2, ProductName = "Product Two"},
            }.AsQueryable());

            //Arrange - Create the controller
            ProductController target = new ProductController(mock.Object);

            //Act - call the GetImage action method
            ActionResult result = target.GetImage(100);

            //Assert
            Assert.IsNull(result);

        }
    }
}
