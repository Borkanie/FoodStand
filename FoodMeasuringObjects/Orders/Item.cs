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

        public Item(Food food)
        {
            Food = food;
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

        public Food Food { get; set; } = new Food()
        {
            Name = "No Food Set",
            Description = "This is a dummy",
            Price = 0            
        };

        private int _quantity = 0;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
            }
        }

        public FoodPortioningType Type { get; set; }

        public int AvailableQuantity()
        {
            return Quantity / Food.WheigthPerPortion;
        }

        public override bool Equals(object? obj)
        {
            if(obj is not Item || obj == null)
            {
                return false;
            }
            Item item = (Item)obj;
            if (item.Food is not null)
                return Food is not null && item.Food! == Food!;
            else
                return Food is null;
        }

        public static bool operator ==(Item left, Item right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Item left, Item right)
        {
            return !left.Equals(right);
        }
    }
}
