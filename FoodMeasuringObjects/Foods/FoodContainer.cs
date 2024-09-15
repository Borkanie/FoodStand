using FoodMeasuringObjects.Telemetry;

namespace FoodMeasuringObjects.Foods
{
    public class FoodContainer
    {
        public FoodContainer(Location location)
        {
            Food = Food.Default;
            Id = Guid.NewGuid();
            Location = location;
        }

        public FoodContainer(Food food, Location location)
        {
            Food = food;
            Id = Guid.NewGuid();
            Location = location;
        }
       

        public Guid Id { get; set; }

        public Food Food { get; set; } 

        public int AvailableQuantity { get; set; } = 0;

        public Location Location { get; }

        public FoodPortioningType Type { get; set; }

        public int AvailablePortions()
        {
            return AvailableQuantity / Food.WheigthPerPortion;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not FoodContainer || obj == null)
            {
                return false;
            }
            FoodContainer item = (FoodContainer)obj;
            if (item.Food is not null)
                return Food is not null && item.Food! == Food!;
            else
                return Food is null;
        }

        public static bool operator ==(FoodContainer left, FoodContainer right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FoodContainer left, FoodContainer right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
