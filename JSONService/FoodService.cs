using FoodMeasuringObjects.Foods;
using Services;

namespace JSONService
{
    public class FoodService : IFoodService
    {
        JSONDatabase<Food> database;
        private Food[] lastFoods;
        public FoodService(string JSONpath)
        {
            database = new JSONDatabase<Food>(JSONpath);
        }

        public Food RegisterFood(string name, string description = "", int wheigth = 0, int wheigthPerPortion = 100, int price = 1)
        {
            Food food = new Food();
            food.Name = name;
            food.Description = description;
            food.Price = price;
            food.WheigthPerPortion = wheigthPerPortion;
            database.Add(food);
            return food;
        }

        public void ResetFoods()
        {
            database.Reset();
        }

        public bool Update(Food newFoodValue)
        {
            Food[] foods = database.GetElements();
            foreach(var food in foods)
            {
                if( food == newFoodValue)
                {
                    database.Remove(food);
                    database.Add(newFoodValue);

                    return true;
                }
            }
            return false;
        }

        public Food[] GetFoods()
        {
            return database.GetElements();
        }

        
    }
}
