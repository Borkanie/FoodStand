using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using Services;
using ServiceUnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JSONServiceUnitTests
{
    public class IOrderServiceUnitTests : ServiceUnitTest
    {
        [Fact]
        public void NewOrderShouldAlwaysHaveNoItems()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var orders = new List<Order>();

            // Act
            for(int i = 0; i < 5; i++)
            {
                orders.Add(orderService.StartNewOrder());
            }

            // Assert
            Assert.Empty(orders.Where(x => x.Items.Count != 0));
        }

        [Fact]
        public void TwoEmptyOrdersShouldStillBeDifferent()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();

            // Act
            var order1 = orderService.StartNewOrder();
            var order2 = orderService.StartNewOrder();

            // Assert
            Assert.True(order1 != order2);
        }

        [Fact]
        public void AddingItemsToOrderShouldIncreasePrice()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default ));
            item.Quantity = 100;
            var order1 = orderService.StartNewOrder();
            
            // Act
            order.Items.Add(item);
            orderService.AddItemToOrder(order1.Id, item);
            order1 = orderService.GetOrder(order1.Id);
            Assert.NotNull(order1);

            // Assert
            Assert.True(order.GetTotalCost() == order1.GetTotalCost());
            Assert.True(order.GetTotalCost() > 0);
        }

        [Fact]
        public void OrderShouldBeEmptyInFrontEndIfItemWasAddedAndBackedInDB()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));
            item.Quantity = 100;
            
            // Act
            var resut = orderService.AddItemToOrder(order.Id, item);


            // Assert
            Assert.Empty(order.Items);
            Assert.True(resut);
        }

        [Fact]
        public void AddItemToOrderShouldReturnFalseIfItemWasNotAddedAndBackedInDB()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));
            item.Quantity = 100;

            // Act
            var resut = orderService.AddItemToOrder(Guid.NewGuid(), item);

            // Assert
            Assert.Empty(order.Items);
            Assert.False(resut);
        }

        [Fact]
        public void GetTotalCostShouldBeUpToDateEvenWhenJustUpdatingObject()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));
            item.Quantity = 100;

            // Act
            order.Items.Add(item);
            orderService.AddItemToOrder(order.Id, item);

            // Assert
            var res1 = order.GetTotalCost();
            var res2 = orderService.GetTotalCost(order.Id);
            Assert.True(res1 == res2);
            Assert.True(order.GetTotalCost() > 0);
        }

        [Fact]
        public void ResetAllOrdersShouldClearAllItemsEvenAddedDirectlyToObject()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));
            item.Quantity = 100;
            var order1 = orderService.StartNewOrder();
            order.Items.Add(item);
            orderService.AddItemToOrder(order1.Id, item);

            // Act
            orderService.ResetAllOrders();

            // Assert
            Assert.True(orderService.GetTotalCost(order.Id) == 0);
            Assert.True(orderService.GetTotalCost(order1.Id) == 0);
        }

        [Fact]
        public void ResetOrdersShouldClearAllItemsFromOrder()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));
            item.Quantity = 100;
            order.Items.Add(item);
            orderService.AddItemToOrder(order.Id, item);

            // Act
            orderService.DeleteOrder(order.Id);

            // Assert
            Assert.True(orderService.GetTotalCost(order.Id) == 0);
            Assert.True(order.GetTotalCost() != 0);
        }

        [Fact]
        public void UpdateItemOnUnexistingOrderShouldReturnFalse()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));

            // Act
            var value = orderService.UpdateItem(Guid.NewGuid(), item);

            // Assert
            Assert.False(value);
        }

        [Fact]
        public void UpdateItemWithUncontianedItemShouldReturnFalse()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));

            // Act
            var value = orderService.UpdateItem(order.Id, item);

            // Assert
            Assert.False(value);
        }

        [Fact]
        public void UpdateItemShouldNotAffectDataBaseWhenAddingDirectly()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));
            item.Quantity = 100;

            // Act
            order.Items.Add(item);
            var real = orderService.GetOrder(order.Id);
            Assert.NotNull(real);

            // Assert
            Assert.NotEqual(real.GetTotalCost(), order.GetTotalCost());
            Assert.Contains(item, order.Items);
            Assert.DoesNotContain(item, real.Items);
        }

        [Fact]
        public void RemoveItemShluldNotRemoveFromDataBseWhenRemoveingDirectly()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));
            item.Quantity = 100;
            orderService.AddItemToOrder(order.Id, item);

            // Act
            order.Items.Remove(item);
            var real = orderService.GetOrder(order.Id);

            // Assert
            Assert.NotEqual(real.GetTotalCost(), order.GetTotalCost());
            Assert.Contains(item, real.Items);
            Assert.DoesNotContain(item, order.Items);    
        }

        [Fact]
        public void RemoveItemWithUncontianedItemShouldReturnFalse()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));

            // Act
            var value = orderService.RemoveItem(order.Id, item);

            // Assert
            Assert.False(value);
        }

        [Fact]
        public void RemoveItemShluldRemoveFromDataBseWhenRemoveingTroughService()
        {
            // Arrange
            var orderService = _container.Resolve<IOrderService>();
            var localService = _container.Resolve<ILocalizationService>();
            var order = orderService.StartNewOrder();
            var item = orderService.CreateItem(localService.GetItemList().First(x => x.Food != Food.Default));
            orderService.AddItemToOrder(order.Id, item);

            // Act
            orderService.RemoveItem(order.Id, item);
            var real = orderService.GetOrder(order.Id);

            // Assert
            Assert.Equal(real.GetTotalCost(), order.GetTotalCost());
            Assert.DoesNotContain(item, real.Items);
            Assert.DoesNotContain(item, order.Items);
        }
    }
}
