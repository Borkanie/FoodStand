namespace FoodMeasuringObjects.Foods
{
    public class Food
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public string Description { get; set; }

        public int Wheigth { get; set; }

        public int WheigthPerPortion { get; set; } = 100;

        public int Price { get; set; } = 1;


    }
}
