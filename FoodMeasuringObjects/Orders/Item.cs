using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Telemetry;

namespace FoodMeasuringObjects.Orders
{
    public class Item
    {
        public Item()
        {
            Id = Guid.NewGuid();
        }

        public Item(ICollection<Location> locations)
        {
            Id = Guid.NewGuid();
            Location.AddRange(locations);
        }

        public Item(Food food, ICollection<Location> locations)
        {
            Food = food;
            Location.AddRange(locations);
            Id = Guid.NewGuid();
        }

        public int Cost
        {
            get
            {
                return Food.Price * Quantity;
            }
        }

        public Guid Id { get; set; }

        public Food Food { get; set; }

        private int _quantity = 0;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity += value / Food.WheigthPerPortion;
            }
        }

        public FoodPortioningType Type { get; set; }

        public int AvailableQuantity()
        {
            return Quantity / Food.WheigthPerPortion;
        }

        public List<Location> Location { get; set; } = new();
    }
}
