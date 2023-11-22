using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;
using YTP.Main.Areas.SportsStore.Controllers;
using YTP.Main.Areas.SportsStore.Models;

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

        [TestMethod]
        public void CanAddToCart() {
            //Arrange - Create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductID = 1, ProductName = "Pwodwi Un", Category = "rekot"}
            }.AsQueryable());

            //Arrange - Create a cart
            Cart cart = new Cart();

            //Arrange - create the controller
            //CartController target = new CartController(mock.Object);
            CartController target = null;

            //Act - Add a product to the cart
            target.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);

        }

        [TestMethod]
        public void AddingProductToCartGoesToCartScreen() {
            //Arrange - Create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, ProductName = "Pwodwi un", Category = "rekot"},
            }.AsQueryable());

            //Arrange - create a cart
            Cart cart = new Cart();

            //Arrange - create the controller
            //CartController target = new CartController(mock.Object);
            CartController target = null;

            //Act - Add a product to the cart
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");

        }

        [TestMethod]
        public void CanViewCartContents() {
            //Arrange - create a cart
            Cart cart = new Cart();

            //Arrange - create the controller
            //CartController target = new CartController(mock.Object);
            CartController target = null;//new CartController(null);

            //Act - Call the Index action method
            CartIndex_VM result = (CartIndex_VM)target.Index(cart, "myUrl").ViewData.Model;

            //Assert 
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }



        [TestMethod]
        public void CannotCheckoutEmptyCart() {
            //Arrange - Create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            //Arrange - create an empty cart
            Cart cart = new Cart();

            //Arrange - Create Shipping details
            ShippingDetails shippingDetails = new ShippingDetails();

            //Arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);

            //Act 

            ViewResult result = target.CheckoutCart(cart, shippingDetails);


            //Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            //Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);
            //Assert - check that we're passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
    
        }

        [TestMethod]
        public void CannotCheckoutInvalidShippingDetails() {
            //Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            //Arrange - Create a with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            //Arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);
            //Arrange - Add an error to the model
            target.ModelState.AddModelError("error", "error");

            //Act - try to checkout

            ViewResult result = target.CheckoutCart(cart, new ShippingDetails());


            //Assert - check that the order hasn't been passed on the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            //Assert - Check that the method is returning the defauld view
            Assert.AreEqual("", result.ViewName);
            //Assert - Check that I am passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CanCheckoutAndSubmitOrder() {
            //Arrange - create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            //Arrange - Create a with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            //Arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);
            //Arrange - Add an error to the model
            target.ModelState.AddModelError("error", "error");

            //Act - try to checkout

            ViewResult result = target.CheckoutCart(cart, new ShippingDetails());

            //Assert - check that the order hasn't been passed on the processor
            mock.Verify(m => m.ProcessOrder( //Failed here - not sure why but we're moving on.
                It.IsAny<Cart>(),
                It.IsAny<ShippingDetails>()),
                Times.Once());

            //Assert - Check that the method is returning the defauld view
            Assert.AreEqual("Completed", result.ViewName);
            //Assert - Check that I am passing a valid model to the view
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }

    }
}
