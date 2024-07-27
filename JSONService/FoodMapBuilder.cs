using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONService
{
    internal class FoodMapBuilder
    {

        private FoodMap foodmap;
        private List<Food> foodList = new();
        private List<Location> locationList = new();
        private FoodMapBuilder() 
        { 
        
        }

        private static FoodMapBuilder instance;
        public static FoodMapBuilder Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new FoodMapBuilder();
                }
                return instance;
            }
        }

        public FoodMapBuilder AddFood(Food food, Location location)
        {
            if (foodList.Contains(food))
            {
                var existingItem = foodmap.Items.First(x => x.Food == food);
                existingItem.Location.Add(location);
            }
            else
            {
                var item = new Item(food, location);
                foodList.Add(food);
                locationList.Add(location);
                if(foodmap == null)
                {
                    foodmap = new FoodMap();
                }

                foodmap.Items.Add(item);    
            }

            return this;
        }

        public FoodMap Finish()
        {
            if(foodmap == null)
            {
                foodmap = new FoodMap();
            }
            foodList.Clear();
            locationList.Clear();
            return foodmap;
        }
    }
}
