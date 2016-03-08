using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Pechka.DLL.Abstract;
using Pechka.DLL.Cncrete;

namespace Pechka.WEB.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUserService>().To<UserRepository>();
            kernel.Bind<IEmailService>().To<EmailService>();
            kernel.Bind<IAdminService>().To<AdminService>();
            kernel.Bind<IScoreService>().To<ScoreService>();
            kernel.Bind<IMenuService>().To<MenuService>();
        }
    }
}