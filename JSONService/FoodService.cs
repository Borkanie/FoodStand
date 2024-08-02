using FoodMeasuringObjects.Foods;
using Services;
using Unity;

namespace JSONService
{
    /// <inheritdoc/>
    public class FoodService : IFoodService
    {
        JSONDatabase<Food> database;
        UnityContainer _container;
        public FoodService(string JSONpath,UnityContainer container)
        {
            _container = container;
            database = new JSONDatabase<Food>(JSONpath);
        }

        /// <inheritdoc/>
        public Food? RegisterFood(string name, string description = "", int wheigthPerPortion = 100, int price = 1)
        {
            Food food = new Food();
            food.Name = name;
            food.Description = description;
            food.Price = price;
            food.WheigthPerPortion = wheigthPerPortion;
            if(database.Add(food))
                return food;
            return null;
        }

        /// <inheritdoc/>
        public bool ResetFoods()
        {
            return database.Reset();
        }

        /// <inheritdoc/>
        public bool Update(Food newFoodValue)
        {
            Food[] foods = database.GetElements();
            foreach(var food in foods)
            {
                if( food == newFoodValue)
                {
                    return database.Remove(food) && database.Add(newFoodValue);
                }
            }
            return false;
        }

        /// <inheritdoc/>
        public Food[] GetFoods()
        {
            return database.GetElements();
        }

        /// <inheritdoc/>
        public bool DeleteFood(Food foodToDelete)
        {
            return database.Remove(foodToDelete);
        }
    }
}
