using FoodMeasuringObjects.Orders;

namespace Services
{
    public interface IOrderService
    {
        public Order StartNewOrder();

        public bool UpdateOrder(Order order);

        public void ResetOrder();

        public int GetTotalCost();

        public Order CloseOrder();

        public bool AddItem(Item item);

        public bool UpdateQuantity(Item item, int newQuantity);
    }
}
