using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;
using YTP.Main.Areas.SportsStore.Controllers;

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

        [TestMethod]
        public void CanEditProduct() {

            //Arrange - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, ProductName = "Product 1"},
                new Product {ProductID = 2, ProductName = "Product 2"},
                new Product {ProductID = 3, ProductName = "Product 3"},
                new Product {ProductID = 4, ProductName = "Product 4"},
                new Product {ProductID = 5, ProductName = "Product 5"},
            });

            //Arrange - Create the controller
            AdminController target = new AdminController(mock.Object);

            //Act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            //Assert
            Assert.AreEqual(1, p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void CannotEditNonExixtentProduct() {

            //Arrange - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, ProductName = "Product 1"},
                new Product {ProductID = 2, ProductName = "Product 2"},
                new Product {ProductID = 3, ProductName = "Product 3"}
            });

            //Arrange - Create the controller
            AdminController target = new AdminController(mock.Object);

            //Act
            Product result = (Product)target.Edit(4).ViewData.Model;

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CanSaveValidChanges() {
            //Arrage - Create mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            
            //Arrage - Create the controller
            AdminController target = new AdminController(mock.Object);

            //Arrange - Create a product
            Product product = new Product { ProductName = "Product One" };

            //Act - Try to save the product
            ActionResult result = target.Edit(product);

            //Assert - Check that the repository was called
            mock.Verify(m => m.SaveProduct(product));
            //Assert - Check the method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CannotSaveInvalidChanges() {
            //Arrange - Create mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();

            //Arrange - Create the controller
            AdminController target = new AdminController(mock.Object);

            //Arrange - Create a product
            Product product = new Product { ProductName = "Product One" };

            //Arrange - Add an error to the model state
            target.ModelState.AddModelError("error", "error");

            //Act - Try to save the product
            ActionResult result = target.Edit(product);

            //Assert - Check that the repository was not called
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            //Assert - Check the method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CanDeleteValidProduct() {
            //Arrange - Create a product
            Product product = new Product { ProductID = 2, ProductName = "Product Name One" };

            //Arrange - Create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, ProductName = "Product Name One"},
                product,
                new Product { ProductID = 3, ProductName = "Product Name Three"}
            });

            //Arrange - Create the controller
            AdminController target = new AdminController(mock.Object);

            //Act - Delete the product
            target.Delete(product.ProductID);

            //Assert - ensure that the repository delete mothod was called with the correct product
            mock.Verify(m => m.DeleteProduct(product.ProductID));
        }
    }
}
