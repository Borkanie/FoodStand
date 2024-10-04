using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;
using JSONService;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace FoodMeasuringAPI
{
    public class BackendAPI
    {
        private UnityContainer _container;
        private static BackendAPI? instance;
        private static string dataBasePath = "D:\\Development\\DBB\\";
        private BackendAPI() 
        {
            _container = new UnityContainer();
            _container.RegisterType<ILocalizationService, LocalizationService>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IFoodService, FoodService>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new object[] { dataBasePath + "debugFood.json" }));
            _container.RegisterType<IOrderService, OrderService>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new object[] { dataBasePath + "debugActive.json", dataBasePath + "debugCache.json" }));
        }

        public UnityContainer Container
        {
            get
            {
                return _container;
            }
        }

        public static BackendAPI Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new BackendAPI();
                }
                return instance;
            }
        }

        public static IFoodService FoodService
        {
            get
            {
                return Instance.Container.Resolve<IFoodService>();  
            }
        }

        public static ILocalizationService LocalizationService
        {
            get
            {
                return Instance.Container.Resolve<ILocalizationService>();
            }
        }

        public static IOrderService OrderService
        {
            get
            {
                return Instance.Container.Resolve<IOrderService>();
            }
        }
    }
}
