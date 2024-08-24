using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Telemetry;

namespace Services
{
    /// <summary>
    /// Deals with the mapping between the registerd foods and the readings from the sensor.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Resets the value of the food map.
        /// </summary>
        public void ResetFoodMap();

        /// <summary>
        /// Ask for the current <see cref="Location"/> of the <see cref="Food"/>
        /// </summary>
        /// <param name="food"></param>
        /// <returns>Returns the lcoation where it was found.Null if it wasn't found.</returns>
        public List<Location> AskForLocation(Food food);

        /// <summary>
        /// Get's a dicitonary of all the changed quantities form the last reading.
        /// The keys are the foods that have changed wheight and the values are the change in wheigth.
        /// If th dictionary is null that means no food was picked up.
        /// </summary>
        /// <returns>A dicitonary of all the foods and the change in mass.</returns>
        public Dictionary<Contianer, int> GetFoodChanges();

        /// <summary>
        /// Return the current populated <see cref="FoodMap"/>
        /// </summary>
        /// <returns></returns>
        public FoodMap GetFoodMap();

        /// <summary>
        /// Returns a list of all populated Items.
        /// </summary>
        /// <returns></returns>
        public List<Contianer> GetItemList();

        /// <summary>
        /// Returns the size of the current <see cref="FoodMap"/>
        /// </summary>
        /// <returns></returns>
        public Tuple<int,int> GetSize();

        /// <summary>
        /// Tries to Add <see cref="Food"/> to the item given by it's <see cref="Location"/>.
        /// </summary>
        /// <param name="food"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool AddFood(Food food, Location location);
    }
}
