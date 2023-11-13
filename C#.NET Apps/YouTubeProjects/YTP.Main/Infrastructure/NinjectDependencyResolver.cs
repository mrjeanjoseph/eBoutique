using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace YTP.Main.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver {
        private readonly IKernel kernel;

    }
}