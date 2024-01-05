using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Mvc;
using YTP.Main.Areas.SportsStore.Controllers;
using YTP.Main.Areas.SportsStore.Models;
using YTP.Main.Infrastructure.Abstract;

namespace YTP.MainTest.SportsStore {
    [TestClass]
    public class AuthTests {

        [TestMethod]
        public void CanLoginWithValidCredentials() {
            //Arrange - Create a mock authentication provider
            Mock<IAuthProvider> provider = new Mock<IAuthProvider>();
            provider.Setup(p => p.Authenticate("admin", "secret")).Returns(true);

            //Arrange - Create the view model
            Login_VM model = new Login_VM {
                UserName = "admin",
                Password = "secret"
            };

            //Arrange  - Create the controller
            AccountController target = new AccountController(provider.Object);

            //Act  - Authenticate using valid credentials
            ActionResult result = target.Login(model, "/MyURL");

            //Assert - 
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void CannotLoginWithInvalidCredentials() {

            //Arrange - Create a mock Authentication provider
            Mock<IAuthProvider> provider = new Mock<IAuthProvider>();
            provider.Setup(p => p.Authenticate("badUser", "badPass")).Returns(false);

            //Arrange - Create the view model
            Login_VM model = new Login_VM {
                UserName = "BadUser",
                Password = "BadPass"
            };

            //Arrange - Create the controller
            AccountController target = new AccountController (provider.Object);

            //Act - Authenticate using provided Credentials
            ActionResult result = target.Login(model, "/MyURL");

            //Assert - 
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

        }
    }
}
