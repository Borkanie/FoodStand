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
    /// <summary>
    /// 
    /// </summary>
    internal class OrderService : IOrderService
    {
        UnityContainer _container;
        JSONDatabase<Order> orders;
        public OrderService(UnityContainer container,string path="orders.dbb")
        {
            _container = container;
            orders = new JSONDatabase<Order>(path);
        }

        /// <inheritdoc/>
        public bool AddItemToOrder(Guid id, Item item)
        {
            var order = orders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return false;
            }
            order.Items.Add(item);
            return orders.Remove(order) && orders.Add(order);
        }

        /// <inheritdoc/>
        public Order? CloseOrder(Guid id)
        {
            var order = orders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return null;
            }
            orders.Remove(order);
            return order;
        }

        /// <inheritdoc/>
        public int GetTotalCost(Guid id)
        {
            var order = orders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return -1;
            }
            
            return order.GetTotalCost();
        }

        /// <inheritdoc/>
        public bool ResetAllOrders()
        {
            return orders.Reset();
        }

        /// <inheritdoc/>
        public bool ResetOrder(Guid id)
        {
            var order = orders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return true;
            }

            return orders.Remove(order);
        }

        /// <inheritdoc/>
        public Order? StartNewOrder()
        {
            Order order = new Order();
            if (orders.Add(order))
            {
                return order;
            }
            return null;
        }

        /// <inheritdoc/>
        public bool UpdateOrder(Order order)
        {
            return orders.Remove(order) && orders.Add(order);
        }

    }
}
