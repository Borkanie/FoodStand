namespace FoodMeasuringObjects.Orders
{
    public class Order
    {
        public Guid Id { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        public void Reset()
        {
            Items.Clear();
        }

        public int GetTotalCost()
        {
            var result = 0;
            foreach (var item in Items)
            {
                result += item.Cost;
            }
            return result;
        }
    }
}
