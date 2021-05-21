using MolDavaBanking.Managers;
using MolDavaBanking.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MolDavaBanking.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserManager, UserManager>();
            //container.RegisterType<IUserRepository, UserRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}