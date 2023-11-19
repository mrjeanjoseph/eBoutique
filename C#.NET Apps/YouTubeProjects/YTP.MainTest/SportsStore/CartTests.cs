using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using YTP.Domain.SportsStore.Entities;

namespace YTP.MainTest.SportsStore.UnitTest {

    [TestClass]
    public class CartTests {

        [TestMethod]
        public void CanAddNewLines() {

            //Arrange - Create some test products
            Product p1 = new Product { ProductID = 1, ProductName = "Product One" };
            Product p2 = new Product { ProductID = 2, ProductName = "Product Two" };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            //Assert
            Assert.AreEqual(results.Length,2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }

        [TestMethod]
        public void CanAddQuantityForExistingLines() {

            //Arrange - Create some test products
            Product p1 = new Product { ProductID = 1, ProductName = "Product One" };
            Product p2 = new Product { ProductID = 2, ProductName = "Product Two" };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.OrderBy(i => i.Product.ProductID).ToArray();

            //Assert
            Assert.AreEqual(results.Length,2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }        

        [TestMethod]
        public void CanRemoveLine() {

            //Arrange - Create some test products
            Product p1 = new Product { ProductID = 1, ProductName = "Product One" };
            Product p2 = new Product { ProductID = 2, ProductName = "Product Two" };
            Product p3 = new Product { ProductID = 3, ProductName = "Product Three" };
            Product p4 = new Product { ProductID = 4, ProductName = "Product Four" };
            Product p5 = new Product { ProductID = 5, ProductName = "Product Five" };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Arrange --Add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //Act
            target.RemoveLine(p2);

            //Assert
            Assert.AreEqual(target.Lines.Where(l => l.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }        

        [TestMethod]
        public void CalculateCartTotal() {

            //Arrange - Create some test products
            Product p1 = new Product { ProductID = 1, ProductName = "Product One", ProductPrice = 150M };
            Product p2 = new Product { ProductID = 2, ProductName = "Product Two", ProductPrice = 200M };
            Product p3 = new Product { ProductID = 3, ProductName = "Product Three", ProductPrice = 100M };
            Product p4 = new Product { ProductID = 4, ProductName = "Product Four", ProductPrice = 75M };
            Product p5 = new Product { ProductID = 5, ProductName = "Product Five", ProductPrice = 120M };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Arrange --Add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.AreEqual(result, 800M);
        }              

        [TestMethod]
        public void CanClearAllItems() {

            //Arrange - Create some test products
            Product p1 = new Product { ProductID = 1, ProductName = "Product One", ProductPrice = 150M };
            Product p2 = new Product { ProductID = 2, ProductName = "Product Two", ProductPrice = 200M };
            Product p3 = new Product { ProductID = 3, ProductName = "Product Three", ProductPrice = 100M };
            Product p4 = new Product { ProductID = 4, ProductName = "Product Four", ProductPrice = 75M };
            Product p5 = new Product { ProductID = 5, ProductName = "Product Five", ProductPrice = 120M };

            //Arrange - Create a new cart
            Cart target = new Cart();

            //Arrange --Add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            //Act - reset the cart
            target.Clear();

            //Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

    }
}
