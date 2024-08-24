using FoodMeasuringObjects.Orders;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JSONService
{
    public class OrderService : IOrderService
    {
        UnityContainer _container;
        JSONDatabase<Order> _confirmedOrders;
        JSONDatabase<Order> _activeOrders;
        public OrderService(UnityContainer container, string path = "_confirmedOrders.json")
        {
            _container = container;
            _confirmedOrders = new JSONDatabase<Order>(path);
            _activeOrders = new JSONDatabase<Order>("orderCache.json");
        }


        /// <inheritdoc/>
        public bool AddItemToOrder(Guid id, Item item)
        {
            var order = _activeOrders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return false;
            }
            order.Items.Add(item);
            return _activeOrders.Remove(order) && _confirmedOrders.Add(order);
        }

        /// <inheritdoc/>
        public Order? CloseOrder(Guid id)
        {
            var order = _activeOrders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return null;
            }
            _activeOrders.Remove(order);
            if (_activeOrders.Add(order))
            {
                return order;
            }
            return null;
        }

        /// <inheritdoc/>
        public int GetTotalCost(Guid id)
        {
            var order = _activeOrders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return -1;
            }
            
            return order.GetTotalCost();
        }

        /// <inheritdoc/>
        public bool ResetAllOrders()
        {
            return _activeOrders.Reset();
        }

        /// <inheritdoc/>
        public bool ResetOrder(Guid id)
        {
            var order = _activeOrders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return true;
            }

            return _activeOrders.Remove(order);
        }

        /// <inheritdoc/>
        public Order? StartNewOrder()
        {
            var order = new Order();
            if (_activeOrders.Add(order))
            {
                return order;
            }
            return null;
        }

        /// <inheritdoc/>
        public bool UpdateOrder(Order order)
        {
            return _activeOrders.Remove(order) && _activeOrders.Add(order);
        }

    }
}
