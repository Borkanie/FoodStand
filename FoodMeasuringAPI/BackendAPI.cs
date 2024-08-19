﻿using FoodMeasuringObjects.Foods;
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

        #region IOrderService
        /// <inheritdoc cref="IOrderService.StartNewOrder()"/>
        public Order StartNewOrder()
        {
            return _container.Resolve<IOrderService>().StartNewOrder();
        }


        /// <inheritdoc cref="IOrderService.UpdateOrder(Order)"/>
        public bool UpdateOrder(Order order)
        {
            return _container.Resolve<IOrderService>().UpdateOrder(order);
        }


        /// <inheritdoc cref="IOrderService.ResetOrder(Guid)"/>
        public bool ResetOrder(Guid id)
        {
            return _container.Resolve<IOrderService>().ResetOrder(id);
        }


        /// <inheritdoc cref="IOrderService.ResetAllOrders()"/>
        public bool ResetAllOrders()
        {
            return _container.Resolve<IOrderService>().ResetAllOrders();
        }

        /// <inheritdoc cref="IOrderService.GetTotalCost(Guid)"/>
        public int GetTotalCost(Guid id)
        {
            return _container.Resolve<IOrderService>().GetTotalCost(id);
        }

        /// <inheritdoc cref="IOrderService.CloseOrder(Guid)"/>
        public Order? CloseOrder(Guid id)
        {
            return _container.Resolve<IOrderService>().CloseOrder(id);
        }

        /// <inheritdoc cref="IOrderService.AddItemToOrder"/>
        public bool AddItemToOrder(Guid orderId, Item item)
        {
            return _container.Resolve<IOrderService>().AddItemToOrder(orderId, item);
        }
        #endregion

        #region ILocalizationService

        /// <inheritdoc cref="ILocalizationService.ResetFoodMap()"/>
        public void ResetFoodMap()
        {
            _container.Resolve<ILocalizationService>().ResetFoodMap();
        }

        /// <inheritdoc cref="ILocalizationService.AskForLocation(Food)"/>
        public Location? AskForLocation(Food food)
        {
            return _container.Resolve<ILocalizationService>().AskForLocation(food);
        }

        /// <inheritdoc cref="ILocalizationService.GetFoodChanges()"/>
        public Dictionary<Item,int> GetFoodChanges()
        {
            return _container.Resolve<ILocalizationService>().GetFoodChanges();
        }


        /// <inheritdoc cref="ILocalizationService.GetFoodMap"/>
        public FoodMap GetFoodMap()
        {
            return _container.Resolve<ILocalizationService>().GetFoodMap();
        }

        #endregion

        #region IFoodService

        /// <inheritdoc cref="IFoodService.GetFoods()"/>
        public Food[] GetFoods()
        {
            return _container.Resolve<IFoodService>().GetFoods();
        }

        /// <inheritdoc cref="IFoodService.ResetFoods()"/>
        public bool ResetFoods()
        {
            return _container.Resolve<IFoodService>().ResetFoods();
        }

        /// <inheritdoc cref="IFoodService.RegisterFood(string, string, int, int)"/>
        public Food? RegisterFood(string name, string description = "", int wheigthPerPortion = 100, int price = 1)
        {
            return _container.Resolve<IFoodService>().RegisterFood(name, description, wheigthPerPortion, price);
        }

        /// <inheritdoc cref="IFoodService.Update(Food)"/>
        public bool Update(Food newFoodValue)
        {
            return _container.Resolve<IFoodService>().Update(newFoodValue);
        }

        /// <inheritdoc cref="IFoodService.DeleteFood(Food)"/>
        public bool DeleteFood(Food foodToDelete)
        {
            return _container.Resolve<IFoodService>().DeleteFood(foodToDelete);
        }

        #endregion

    }
}
