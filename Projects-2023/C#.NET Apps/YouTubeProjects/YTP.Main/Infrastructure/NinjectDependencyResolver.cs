using Moq;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Concrete;
using YTP.Domain.SportsStore.Entities;
using YTP.Main.Areas.SandboxTBP.Models;
using YTP.Main.Infrastructure.Abstract;
using YTP.Main.Infrastructure.Concrete;

namespace YTP.Main.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver {

        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam) {
            _kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings() {
            _kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();
            _kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithConstructorArgument("DiscountSize", 50M);

            _kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();

            //_kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>();
            //_kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 50M);

            //SportsStore Implementation
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> { 
                new Product { ProductName = "Football", ProductPrice = 28},
                new Product { ProductName = "Basketball", ProductPrice = 19},
                new Product { ProductName = "Volleyball", ProductPrice = 26},
                new Product { ProductName = "pingpongball", ProductPrice = 22},
                new Product { ProductName = "testiball", ProductPrice = 29}
            });

            //_kernel.Bind<IProductsRepository>().ToConstant(mock.Object);

            //Service request from for the IProductRepository Interface
            _kernel.Bind<IProductsRepository>().To<EFProductRepository>().InRequestScope();
            EmailSettings emailSettings = new EmailSettings {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false"),
                //Username = ConfigurationManager.AppSettings["Email.UserName"] ?? string.Empty, //Maybe we can pass in those values here too
            };

            _kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            _kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }

        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }
    }
}