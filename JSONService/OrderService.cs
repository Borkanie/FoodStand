using FoodMeasuringObjects.Foods;
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
        public OrderService(UnityContainer container, string path = "_confirmedOrders.json", string tempCach = "orderCache.json")
        {
            _container = container;
            _confirmedOrders = new JSONDatabase<Order>(path);
            _activeOrders = new JSONDatabase<Order>(tempCach);
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
            return _activeOrders.Remove(order) && _activeOrders.Add(order);
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
        public Item CreateItem(FoodContainer source)
        {
            return new Item(source.Food);
        }

        /// <inheritdoc/>
        public int GetTotalCost(Guid id)
        {
            var order = _activeOrders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return 0;
            }

            return order.GetTotalCost();
        }

        /// <inheritdoc/>
        public bool ResetAllOrders()
        {
            return _activeOrders.Reset();
        }

        /// <inheritdoc/>
        public bool DeleteOrder(Guid id)
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
                return new Order()
                {
                    Id = order.Id,
                };
            }
            return null;
        }

        /// <inheritdoc/>
        public bool UpdateItem(Guid id, Item item)
        {
            var order = _activeOrders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null || !order.Items.Contains(item) || order.Items.Remove(item))
            {
                return false;
            }
            order.Items.Add(item);
            return _activeOrders.Update(order);
        }

        /// <inheritdoc/>
        public bool RemoveItem(Guid id, Item item)
        {

            var order = _activeOrders.GetElements().FirstOrDefault(x => x.Id == id);
            if (order == null || !order.Items.Contains(item) || order.Items.Remove(item))
            {
                return false;
            }
            
            return _activeOrders.Update(order);
        }

        public Order? GetOrder(Guid id)
        {
            if(_activeOrders.GetElements().FirstOrDefault(_x => _x.Id == id) is not null)
            {
                return _activeOrders.GetElements().First(x => x.Id == id);  
            }
            if(_confirmedOrders.GetElements().FirstOrDefault(_x => _x.Id == id) is not null)
            {
                return _confirmedOrders.GetElements().First(x => x.Id == id);
            }
            return null;
        }
    }
}
