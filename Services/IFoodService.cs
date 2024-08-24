using FoodMeasuringObjects.Foods;

namespace Services
{
    /// <summary>
    /// Register and manages food types for the user.
    /// </summary>
    public interface IFoodService
    {
        /// <summary>
        /// Add a new food to the inventory.
        /// </summary>
        /// <param name="name">The name of the food to be added.</param>
        /// <param name="description"></param>
        /// <param name="wheigthPerPortion"></param>
        /// <param name="price"></param>
        /// <returns>The instance that has been registered in the database.
        /// Null if it wasn't saved.</returns>
        public Food? RegisterFood(string name, string description = "", int wheigthPerPortion = 100, int price = 1);

        /// <summary>
        /// Updates the values of the Food item.
        /// It will dientify the item by name and will update all values accordingly.
        /// </summary>
        /// <param name="newFoodValue"></param>
        /// <returns><see cref="true"/> if it was succesfully added.</returns>
        public bool Update(Food newFoodValue);

        /// <summary>
        /// Resets the food list completely.
        /// Warning it will also clean up the database.
        /// </summary>
        public bool ResetFoods();

        /// <summary>
        /// Removes a <see cref="Food"/> from the database.
        /// </summary>
        /// <param name="foodToDelete"></param>
        /// <returns>True if the element was succesfully removed.</returns>
        public bool DeleteFood(Food foodToDelete);

        /// <summary>
        /// Removes all the foods as an array.
        /// </summary>
        /// <returns></returns>
        public Food[] GetFoods();

        /// <summary>
        /// Returns the food that hase as a name the given parameter.
        /// </summary>
        /// <param name="name"></param>
        /// <returns> Null if no element has that name in the database.</returns>
        public Food? Get(string name);

    }
}
