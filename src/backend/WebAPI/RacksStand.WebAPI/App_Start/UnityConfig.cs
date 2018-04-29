
using Microsoft.Practices.Unity;
using Repository.Core;
using Repository.Interfaces;
using Repository.Repository;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RacksStand.WebAPI
{
    public static class UnityConfig
    {

        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            // register all your components with the container here
            ConfigureRepositories(container);
            ConfigureServices(container);
          
            config.DependencyResolver = new UnityResolver(container);
        }
        private static void ConfigureServices(UnityContainer container)
        {
            container.RegisterType<IAccountService, AccountService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISessionService, SessionService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILoginHistoryService, LoginHistoryService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmailServerSettingService, EmailServerSettingService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITokenService, TokenService>(new HierarchicalLifetimeManager());
            container.RegisterType<IActionLogService, ActionLogService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerService, CustomerService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICurrencyService, CurrencyService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaxService, TaxService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentTermService, PaymentTermService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplierService, SupplierService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStoreService, StoreService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoomService, RoomService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRackService, RackService>(new HierarchicalLifetimeManager());
            container.RegisterType<IInventoryService, InventoryService>(new HierarchicalLifetimeManager());

        }
        private static void ConfigureRepositories(UnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISessionRepository, SessionRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILoginHistoryRepository, LoginHistoryRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITokenRepository, TokenRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IEmailServerRepository, EmailServerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IActivityLogRepository, ActivityLogRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerRepository, CustomerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICurrencyRepository, CurrencyRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaxRepository, TaxRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentTermRepository, PaymentTermRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISupplierRepository, SupplierRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IStoreRepository, StoreRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoomRepository, RoomRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IRackRepository, RackRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IInventoryRepository, InventoryRepository>(new HierarchicalLifetimeManager());
            //database connection setup.
            container.RegisterType<IDbConnectionProvider, SqlConnectionProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IConnectionStringProvider, ConnectionStringProvider>(new HierarchicalLifetimeManager());
        }
    }
}