using FoodMeasuringObjects.Foods;

namespace Services
{
    public interface IFoodService
    {
        public Food RegisterFood(string name, string description = "", int wheigth = 0, int wheigthPerPortion = 100, int price = 1);

        public bool Update(Food newFoodValue);

        public void ResetFoods();
    }
}
