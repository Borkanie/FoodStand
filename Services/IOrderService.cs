using FoodMeasuringObjects.Orders;

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
        /// Send changes to the order.
        /// </summary>
        /// <param name="order">The new valeus of the order.</param>
        /// <returns>True if the change has been saved into the database.</returns>
        public bool UpdateOrder(Order order);

        /// <summary>
        /// Resets all the items in an order.
        /// </summary>
        /// <returns>Returns True if the order was succesfully removed from the database.</returns>
        public bool ResetOrder(Order order);

        /// <summary>
        /// Removes all orders.
        /// </summary>
        /// <returns>Clears all ongoing orders from the database.</returns>
        public bool ResetAllOrders();

        /// <summary>
        /// Calculate total price for an order.
        /// </summary>
        /// <returns>Returns the total price for an order.</returns>
        public int GetTotalCost(Order order);

        /// <summary>
        /// Closes a given order.
        /// </summary>
        /// <returns>True if it was succesfully closed.</returns>
        public bool CloseOrder(Order order);

        /// <summary>
        /// Adds an item to an order.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="item"></param>
        /// <returns>True if the order was succesfully modified.</returns>
        public bool AddItem(Order order,Item item);

        /// <summary>
        /// Forcefully updates quantity for a given item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="newQuantity"></param>
        /// <returns>True if it was succesfully modified.</returns>
        public bool UpdateQuantity(Item item, int newQuantity);
    }
}
