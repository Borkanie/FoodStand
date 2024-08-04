using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;
using JSONService;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace FoodMeasuringAPI
{
    public class BackendAPI
    {
        private UnityContainer _container;
        private static BackendAPI instance;
        private BackendAPI() 
        {
            _container = new UnityContainer();
            var injection = new InjectionConstructor(_container);
            _container.RegisterSingleton<IFoodService, FoodService>(injection);
            _container.RegisterSingleton<ISensorReadingService, SensorMockingService>(injection);
            _container.RegisterSingleton<ILocalizationService, LocalizationService>(injection);
            _container.RegisterSingleton<IOrderService, OrderService>(injection);
            
        }

        #region IOrderService
        /// <inheritdoc cref="IOrderService.StartNewOrder()"/>
        public static Order StartNewOrder()
        {
            return instance._container.Resolve<IOrderService>().StartNewOrder();
        }


        /// <inheritdoc cref="IOrderService.UpdateOrder(Order)"/>
        public static bool UpdateOrder(Order order)
        {
            return instance._container.Resolve<IOrderService>().UpdateOrder(order);
        }


        /// <inheritdoc cref="IOrderService.ResetOrder(Guid)"/>
        public static bool ResetOrder(Guid id)
        {
            return instance._container.Resolve<IOrderService>().ResetOrder(id);
        }


        /// <inheritdoc cref="IOrderService.ResetAllOrders()"/>
        public static bool ResetAllOrders()
        {
            return instance._container.Resolve<IOrderService>().ResetAllOrders();
        }

        /// <inheritdoc cref="IOrderService.GetTotalCost(Guid)"/>
        public static int GetTotalCost(Guid id)
        {
            return instance._container.Resolve<IOrderService>().GetTotalCost(id);
        }

        /// <inheritdoc cref="IOrderService.CloseOrder(Guid)"/>
        public static Order? CloseOrder(Guid id)
        {
            return instance._container.Resolve<IOrderService>().CloseOrder(id);
        }

        /// <inheritdoc cref="IOrderService.AddItemToOrder"/>
        public static bool AddItemToOrder(Guid orderId, Item item)
        {
            return instance._container.Resolve<IOrderService>().AddItemToOrder(orderId, item);
        }
        #endregion

        #region ILocalizationService

        /// <inheritdoc cref="ILocalizationService.ResetFoodMap()"/>
        public static void ResetFoodMap()
        {
            instance._container.Resolve<ILocalizationService>().ResetFoodMap();
        }

        /// <inheritdoc cref="ILocalizationService.AskForLocation(Food)"/>
        public static Location? AskForLocation(Food food)
        {
            return instance._container.Resolve<ILocalizationService>().AskForLocation(food);
        }

        /// <inheritdoc cref="ILocalizationService.GetFoodChanges()"/>
        public static Dictionary<Item,int> GetFoodChanges()
        {
            return instance._container.Resolve<ILocalizationService>().GetFoodChanges();
        }

        #endregion

    }
}
