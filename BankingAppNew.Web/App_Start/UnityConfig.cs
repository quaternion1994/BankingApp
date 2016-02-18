using BankingAppNew.Core;
using BankingAppNew.Core.Services;
using BankingAppNew.Core.Services.Impl;
using BankingAppNew.DataAccess.Repositories;
using BankingAppNew.DataAccess.UnitsOfWork.Impl;
using Microsoft.Practices.Unity;
using System;
using System.Web.Http;
using Unity.WebApi;

namespace BankingAppNew.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>();
            container.RegisterType<IBankService, BankService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(UnityConfig.container);
        }

        private static IUnityContainer container = new UnityContainer();
        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container;
        }
    }
}