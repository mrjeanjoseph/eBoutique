using Moq;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using YTP.Domain.Abstract;
using YTP.Domain.SportsStore.Entities;
using YTP.Main.Areas.SandboxTBP.Models;

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

            _kernel.Bind<IProductsRepository>().ToConstant(mock.Object);
        }

        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }
    }
}