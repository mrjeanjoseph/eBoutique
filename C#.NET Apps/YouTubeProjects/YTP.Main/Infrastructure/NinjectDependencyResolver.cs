using Ninject;
using System;
using System.Collections.Generic;

using System.Web.Http.Dependencies;
using YTP.Main.Models;

namespace YTP.Main.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver, System.Web.Mvc.IDependencyResolver {
        private readonly IKernel kernel;

        public NinjectDependencyResolver() {
            kernel = new StandardKernel();
            AddBidings();
        }

        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        private void AddBidings() {
            kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
        }

        public IDependencyScope BeginScope() {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}