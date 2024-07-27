using FoodMeasuringObjects.Foods;

namespace Services
{
    public interface IFoodService
    {
        public Food RegisterFood(string name, string description = "", int wheigth = 0, int wheigthPerPortion = 100, int price = 1);

        public bool Update(Food newFoodValue);

        public void ResetFoods();

        public Food[] GetFoods();

        /// <summary>
        /// Get's a dicitonary of all the changed quantities form the last reading.
        /// The keys are the foods that have changed wheight and the values are the change in wheigth.
        /// If th dictionary is null that means no food was picked up.
        /// </summary>
        /// <returns>A dicitonary of all the foods and the change in mass.</returns>
        public Dictionary<Food,int> GetFoodChanges();
    }
}
