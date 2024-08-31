using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using System;

namespace Services
{
    /// <summary>
    /// Deals with order registration and item management in the context of an order.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Starts a new Order for a client.
        /// </summary>
        /// <returns>A reference to the order.</returns>
        public Order StartNewOrder();

        /// <summary>
        /// Send changes to the order and try to save them in backend.
        /// </summary>
        /// <param name="id">The identifier of the targeted order.</param>
        /// <param name="item">The new values that will override the current valeus in the database.</param>
        /// <returns>True if the change has been saved into the database.</returns>
        public bool UpdateItem(Guid id, Item item);

        /// <summary>
        /// Removes an item from an order.
        /// </summary>
        /// <param name="id">The identifier of the targeted order.</param>
        /// <param name="item">The <see cref="Item"/> that will be removed from the order.
        /// it will be identified by the equal operator so by it's name.</param>
        /// <returns>True if the change has been saved into the database.</returns>
        public bool RemoveItem(Guid id, Item item);

        /// <summary>
        /// Resets all the items in an order.
        /// </summary>
        /// <returns>Returns True if the order was succesfully removed from the database.</returns>
        public bool DeleteOrder(Guid id);

        /// <summary>
        /// Removes all orders.
        /// </summary>
        /// <returns>Clears all ongoing orders from the database.</returns>
        public bool ResetAllOrders();

        /// <summary>
        /// Calculate total price for an order.
        /// </summary>
        /// <returns>Returns the total price for an order.</returns>
        public int GetTotalCost(Guid id);

        /// <summary>
        /// Closes a given order.
        /// </summary>
        /// <returns>True if it was succesfully closed.</returns>
        public Order? CloseOrder(Guid id);

        /// <summary>
        /// Adds an item to an order.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>True if the order was succesfully modified.</returns>
        public bool AddItemToOrder(Guid orderId,Item item);

        /// <summary>
        /// Creates an item from the food in a given container.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Item CreateItem(FoodContainer source);

        /// <summary>
        /// Returns an order identified by it's Id form the database.
        /// </summary>
        /// <param name="id">id of the order.</param>
        /// <returns>Return null fi no roder is found.</returns>
        public Order? GetOrder(Guid id);
    }
}
