using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;

namespace Services
{
    public interface ILocalizationService
    {
        public FoodMap GetFoodMap();

        public void ResetFoodMap();

        public bool Update(Location location);

        public Location AskForLocation(Food food);

        /// <summary>
        /// Get's a dicitonary of all the changed quantities form the last reading.
        /// The keys are the foods that have changed wheight and the values are the change in wheigth.
        /// If th dictionary is null that means no food was picked up.
        /// </summary>
        /// <returns>A dicitonary of all the foods and the change in mass.</returns>
        public Dictionary<Item, int> GetFoodChanges();
    }
}
