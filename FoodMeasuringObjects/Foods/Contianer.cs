using FoodMeasuringObjects.Telemetry;

namespace FoodMeasuringObjects.Foods
{
    public class Contianer
    {
        public Contianer()
        {
            Id = Guid.NewGuid();
        }

        public Contianer(Food food)
        {
            Food = food;
            Id = Guid.NewGuid();
        }
       

        public Guid Id { get; set; }

        public Food Food { get; set; } = Food.Default;

        public int AvailableQuantity { get; set; } = 0;

        public FoodPortioningType Type { get; set; }

        public int AvailablePortions()
        {
            return AvailableQuantity / Food.WheigthPerPortion;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Contianer || obj == null)
            {
                return false;
            }
            Contianer item = (Contianer)obj;
            if (item.Food is not null)
                return Food is not null && item.Food! == Food!;
            else
                return Food is null;
        }

        public static bool operator ==(Contianer left, Contianer right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Contianer left, Contianer right)
        {
            return !left.Equals(right);
        }
    }
}
