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
    internal class LocalizationService : ILocalizationService
    {
        private FoodMap _foodMap;
        private UnityContainer _container;
    
        public LocalizationService(UnityContainer serviceContainer)
        {
            _container = serviceContainer;
        }

        /// <summary>
        /// Get's the food map after a reset.
        /// </summary>
        /// <returns></returns>
        private FoodMap GetFoodMap()
        {
            if(_foodMap is null)
                ResetFoodMap();
            return _foodMap;
        }

        /// <inheritdoc/>
        public void ResetFoodMap()
        {
            _foodMap = _container.Resolve<ISensorReadingService>().getLatestReadings();
             
            foreach (var food in _container.Resolve<IFoodService>().GetFoods())
            {
                var location = AskForLocation(food);
                _foodMap.AddFood(food, location);
            }            
        }

        /// <inheritdoc/>
        public Location? AskForLocation(Food food)
        {

            for(int i = 0; i < _foodMap.ElementsOnLine; i++)
            {
                for(int j=0;j< _foodMap.ElementsOnColumn; j++)
                {
                    if(_foodMap.Get(i,j) == null)
                    {
                        return new Location() { Line = i, Column = j };
                    }
                }
            }
            return null;
        }

        /// <inheritdoc/>
        public bool Update(Location location)
        {
            var oldItem = _foodMap.Get(location);
            if(oldItem != null)
            {
                oldItem.Location.Add(location);
                return true;

            }
            return false;
        }

        /// <summary>
        /// This method will reset the current map and try to apply the same
        /// foods based on the location in the recieved mapping.
        /// </summary>
        /// <param name="oldFoodMap">The current locaiton of the foods we want to preserve.</param>
        private void ReadNewQuantitiesAndReapplyMapping(FoodMap oldFoodMap)
        {
            ResetFoodMap();
            if(_foodMap.ElementsOnLine != oldFoodMap.ElementsOnLine || _foodMap.ElementsOnColumn != oldFoodMap.ElementsOnColumn)
            {
                throw new InvalidOperationException("We cannot change the size of the map from this method");
            }

            for (int line = 0; line < _foodMap.ElementsOnLine; line++)
            {
                for (int column = 0; column < _foodMap.ElementsOnColumn; column++)
                {
                    var food = oldFoodMap.Get(line, column);
                    if(food != null)
                    {
                        _foodMap.AddFood(food.Food, new Location() { Column = column, Line = line });
                    }
                }
            }

        }

        /// <inheritdoc/>
        public Dictionary<Item, int> GetFoodChanges()
        {
            var oldFoodMap = _foodMap; 
            ReadNewQuantitiesAndReapplyMapping(oldFoodMap);

            var foods = new Dictionary<Item, int>();
            
            
            foreach (var food in _foodMap.GetItemList())
            {
                var oldfood = oldFoodMap.Get(food.Location.First());

                if(oldfood.Quantity - food.Quantity < 0)
                {
                    foods.Add(food, oldfood.Quantity - food.Quantity);
                }
            }
            return foods;
        }
    }
}
