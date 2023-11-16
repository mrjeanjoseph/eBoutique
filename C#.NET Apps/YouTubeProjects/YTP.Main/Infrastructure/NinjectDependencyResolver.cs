using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
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
        }

        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }
    }
}