namespace FoodMeasuringObjects.Foods
{
    public class Food
    {
        public String Name { get; set; } = "";

        public string Description { get; set; } = "";

        public int WheigthPerPortion { get; set; } = 100;

        public int Price { get; set; } = 1;

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if(obj is not Food || obj == null)
            {
                return false;
            }
            var food = (Food)obj;
            return string.Equals(food.Name, Name);
        }

        public static bool operator ==(Food lhs, Food rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Food lhs, Food rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}
