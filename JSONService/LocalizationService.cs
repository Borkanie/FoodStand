using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;
using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Unity;
using Unity.Injection;

namespace JSONService
{
    /// <inheritdoc/>
    public class LocalizationService : ILocalizationService
    {
        private FoodMap _foodMap;
        private UnityContainer _container;

        public LocalizationService(UnityContainer serviceContainer)
        {
            _container = serviceContainer;
        }

        /// <inheritdoc/>
        public FoodMap FoodMap
        {
            get{
                if(_foodMap is null)
                    ResetFoodMap();
                return _foodMap;
            }
        }

        /// <inheritdoc/>
        public void ResetFoodMap()
        {
            if(_foodMap is not null)
            {
                FoodMap oldMap = _foodMap;
                _foodMap = _container.Resolve<ISensorReadingService>().getLatestReadings();
                foreach (var food in _container.Resolve<IFoodService>().GetFoods())
                {
                    var location = oldMap.AskForLocation(food);
                    foreach (var loc in location)
                        FoodMap.AddFood(food, loc);
                }
            }
            else
            {
                _foodMap = _container.Resolve<ISensorReadingService>().getLatestReadings();
            }           
        }

        /// <inheritdoc/>
        public List<Location> AskForLocation(Food food)
        {
            List<Location> locations = new List<Location>();
            for(int i = 0; i < FoodMap.ElementsOnLine; i++)
            {
                for(int j=0;j< FoodMap.ElementsOnColumn; j++)
                {
                    if(FoodMap.Get(i, j) is not null &&
                        FoodMap.Get(i,j)!.Food is not null && 
                        FoodMap.Get(i, j)!.Food! == food)
                    {
                        locations.Add( new Location() { Line = i, Column = j });
                    }
                }
            }
            return locations;
        }

        /// <summary>
        /// This method will reset the current map and try to apply the same
        /// foods based on the location in the recieved mapping.
        /// </summary>
        /// <param name="oldFoodMap">The current locaiton of the foods we want to preserve.</param>
        private void ReadNewQuantitiesAndReapplyMapping(FoodMap oldFoodMap)
        {
            ResetFoodMap();
            if(FoodMap.ElementsOnLine != oldFoodMap.ElementsOnLine || FoodMap.ElementsOnColumn != oldFoodMap.ElementsOnColumn)
            {
                throw new InvalidOperationException("We cannot change the size of the map from this method");
            }

            for (int line = 0; line < FoodMap.ElementsOnLine; line++)
            {
                for (int column = 0; column < FoodMap.ElementsOnColumn; column++)
                {
                    var food = oldFoodMap.Get(line, column);
                    if(food is null)
                    {
                        FoodMap.AddFood(food.Food, new Location() { Column = column, Line = line });
                    }
                }
            }

        }

        /// <inheritdoc/>
        public Dictionary<Item, int> GetFoodChanges()
        {
            var oldFoodMap = FoodMap; 
            ReadNewQuantitiesAndReapplyMapping(oldFoodMap);

            var foods = new Dictionary<Item, int>();

            for (int i = 0; i < FoodMap.ElementsOnLine; i++){
                for(int j=0; j < FoodMap.ElementsOnColumn; j++)
                {
                    var oldFoood = oldFoodMap.Get(i, j);
                    if (oldFoood.Food is not null){
                        var newValue = FoodMap.Get(i, j);
                        foods.Add(newValue, oldFoood.Quantity - newValue.Quantity);
                    }
                }
            }
            
            return foods;
        }

        public List<Item> GetItemList()
        {
            var list = new List<Item>();
            foreach(var item in FoodMap.GetItemList())
            {
                var newItem = new Item()
                {
                    Food = item.Food,
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Type = item.Type,
                };
                list.Add(newItem);
            }
            return list;
        }

        public Tuple<int, int> GetSize()
        {
            return new Tuple<int,int>(
                FoodMap.ElementsOnLine,FoodMap.ElementsOnColumn);
        }

        public bool AddFood(Food food, Location location)
        {
            if (FoodMap.ElementsOnLine <= location.Line
               || FoodMap.ElementsOnColumn <= location.Column)
                return false;
            FoodMap.AddFood(food,location);
            return true;
        }

        public FoodMap GetFoodMap()
        {
            return FoodMap.Clone();
        }
    }
}
