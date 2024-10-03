using JSONService;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Injection;
using Unity.Lifetime;
using Unity;

namespace ServiceUnitTests
{
    public class ServiceUnitTest : IDisposable
    {
        protected UnityContainer _container;
        private int random;
        public ServiceUnitTest()
        {
            random = new Random().Next();
            if (!Directory.Exists(random.ToString()))
            {
                Directory.CreateDirectory(random.ToString());
            }
            _container = new UnityContainer();
            _container.RegisterType<ILocalizationService, LocalizationService>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IFoodService, FoodService>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new object[] {random.ToString() + "/TestFoodService.json" }));
            _container.RegisterType<IOrderService, OrderService>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(new object[] {random.ToString() + "/TestOrder.json", random.ToString() + "OrderCache.json" }));
        }

        public void Dispose()
        {
            if (Directory.Exists(random.ToString()))
            {
                Directory.Delete(random.ToString(), true);
            }

            if (Directory.Exists(random.ToString()))
            {
                Directory.Delete(random.ToString(), true);
            }
        }
    }
}
