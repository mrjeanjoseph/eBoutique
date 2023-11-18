using Microsoft.VisualStudio.TestTools.UnitTesting;
using YTP.Main.Areas.SportsStore.Models.HtmlHelpers;
using YTP.Main.Areas.SportsStore.Controllers;
using YTP.Main.Areas.SportsStore.Models;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;
using System.Web.Mvc;
using System.Linq;
using System;
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
            //IEnumerable<Product> result = (IEnumerable<Product>)controller.ListProducts(2); //
            ProductsList_VM result = (ProductsList_VM)controller.ListProducts(null, 2).Model;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].ProductName, "P4");
            Assert.AreEqual(prodArray[1].ProductName, "P5");
        }

        [TestMethod]
        public void CanGeneratePageLinks() {
            //Arrange - Define an HTML Helper - We need this to do this 
            //in order to apply the extension method
            HtmlHelper myHelper = null;

            //Arrange - create Paging Info data
            PagingInfo pagingInfo = new PagingInfo {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            //Arrange - Setup the delegate using a lambda express
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //Assert
            Assert.AreEqual(@"<a class='btn btn-default' href='Page1'>1</a><a class='btn btn-default btn-primary selected' href='Page2'>2</a><a class='btn btn-default' href='Page3'>3</a>", result.ToString());
        }

        [TestMethod]
        public void CanSendPaginationViewModel() {

            //Arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, ProductName = "P1"},
                new Product {ProductID = 2, ProductName = "P2"},
                new Product {ProductID = 3, ProductName = "P3"},
                new Product {ProductID = 4, ProductName = "P4"},
                new Product {ProductID = 5, ProductName = "P5"}
            });

            //Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            ProductsList_VM result = (ProductsList_VM)controller.ListProducts(null, 2).Model;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void CanFilterProducts() {
            //Arrage
            //- Create the repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, ProductName = "P1"},
                new Product {ProductID = 2, ProductName = "P2"},
                new Product {ProductID = 3, ProductName = "P3"},
                new Product {ProductID = 4, ProductName = "P4"},
                new Product {ProductID = 5, ProductName = "P5"}
            });

            //Arrange
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            Product[] result = ((ProductsList_VM)controller.ListProducts(null, 2).Model).Products.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].ProductName == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].ProductName == "P4" && result[1].Category == "Cat2");
        }
    }
}
