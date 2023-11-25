﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Web.Routing;
using Moq;
using System;
using System.Web;
using YTP.Main;

namespace YTP.MainTest.UrlsAndRoutes {
    [TestClass]
    public class UnitTest1 {

        [TestMethod]
        public void TestIncomingRoutes2() {
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "Index");
            TestRouteMatch("~/Customer/List", "Customer", "Index");
            TestRouteMatch("~/Customer/List/All", "Customer", "Index");
        }


        [TestMethod]
        public void TestIncomingRoutes() {
            //Check for the url that is hoped for
            TestRouteMatch("~/Admin/Index", "Admin", "Index");

            //Check that the values are being obtained from the segments
            TestRouteMatch("~/One/Two/", "One", "Two");

            //Ensure that too many or twoo few segments fails to match
            TestRouteFail("~/Admin/Index/Segment");
            TestRouteFail("~/Admin");
        }


        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET") {
            //Create the mock request
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            //Create the mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            //Create the mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            //Return the mocked context
            return mockContext.Object;
        }

        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null, string httpMethod = "GET") {
            //Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Act - Process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null) {

            Func<object, object, bool> valCompare = (v1, v2) => {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };

            bool result = valCompare(routeResult.Values["controller"], controller)
                && valCompare(routeResult.Values["action"], action);

            if (propertySet != null) {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo prop in propInfo) {
                    if (!(routeResult.Values.ContainsKey(prop.Name) && valCompare(routeResult.Values[prop.Name], prop.GetValue(propertySet, null)))) {
                        result = false; break;
                    }
                }
            }

            return result;
        }
        private void TestRouteFail(string url) {
            //Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Act - Process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            //Assert
            Assert.IsTrue(result == null || result.Route == null);
        }
    }
}
