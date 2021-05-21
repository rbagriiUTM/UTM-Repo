using Autofac;
using Autofac.Integration.Mvc;
using MolDavaBanking.Managers;
using MolDavaBanking.Managers.Interfaces;
using MolDavaBanking.Managers.Managers;
using MolDavaBanking.Repositories;
using MolDavaBanking.Repositories.Interfaces;
using MolDavaBanking.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MolDavaBanking.Web
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            //builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            //builder.InjectActionInvoker();

            // Set the dependency resolver to be Autofac.
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();
            builder.RegisterType<CardRepository>().As<ICardRepository>();
            builder.RegisterType<BankRepository>().As<IBankRepository>();
            builder.RegisterType<SupportRepository>().As<ISupportRepository>();

            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<TransactionManager>().As<ITransactionManager>();
            builder.RegisterType<CardManager>().As<ICardManager>();
            builder.RegisterType<BankManager>().As<IBankManager>();
            builder.RegisterType<SupportManager>().As<ISupportManager>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}