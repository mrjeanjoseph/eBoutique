using Ninject;
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
            _kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
        }

        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }
    }
}