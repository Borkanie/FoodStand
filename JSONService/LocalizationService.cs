using FoodMeasuringObjects.Foods;
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
        private FoodMap? _foodMap;
        private SensorMockingService sensorMockingService;

        public LocalizationService()
        {
            sensorMockingService = new SensorMockingService();
        }

        /// <inheritdoc/>
        public FoodMap FoodMap
        {
            get{
                if(_foodMap is null)
                    ResetFoodMap();
                return _foodMap!;
            }
        }

        /// <inheritdoc/>
        public void ResetFoodMap()
        {
            if(_foodMap is not null)
            {
                FoodMap oldMap = _foodMap;
                _foodMap = sensorMockingService.getLatestReadings();
                foreach (var container in _foodMap.GetItemList())
                {
                    var location = oldMap.AskForLocation(container.Food);
                    foreach (var loc in location)
                        FoodMap.AddFood(container.Food, loc);
                }
            }
            else
            {
                _foodMap = sensorMockingService.getLatestReadings();
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
                    if(food is not null)
                    {
                        FoodMap.AddFood(food.Food, new Location() { Column = column, Line = line });
                    }
                }
            }

        }

        /// <inheritdoc/>
        public Dictionary<FoodContainer, int> GetFoodChanges()
        {
            var oldFoodMap = FoodMap; 
            ReadNewQuantitiesAndReapplyMapping(oldFoodMap);

            var foods = new Dictionary<FoodContainer, int>();

            for (int i = 0; i < FoodMap.ElementsOnLine; i++){
                for(int j=0; j < FoodMap.ElementsOnColumn; j++)
                {
                    var oldFoood = oldFoodMap.Get(i, j);
                    if (oldFoood.Food is not null){
                        var newValue = FoodMap.Get(i, j);
                        foods.Add(newValue, oldFoood.AvailableQuantity - newValue.AvailableQuantity);
                    }
                }
            }
            
            return foods;
        }

        public List<FoodContainer> GetItemList()
        {
            var list = new List<FoodContainer>();
            foreach(var item in FoodMap.GetItemList())
            {
                var newItem = new FoodContainer(item.Location)
                {
                    Food = item.Food,
                    Id = item.Id,
                    AvailableQuantity = item.AvailableQuantity,
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

        private class SensorMockingService
        {
            private FoodMap MakeReading()
            {
                var foods = new[]
                {
                new Food()
                {
                    Name = "Baked Potato",
                    Description = "Something",
                    WheigthPerPortion = 1000,
                    Price = 123
                },
                new Food()
                {
                    Name = "Raw Chicken",
                    Description = "Has salmonella",
                    WheigthPerPortion = 100,
                    Price = 12
                },
                new Food()
                {
                    Name = "Pasta",
                    Description = "Is actually good",
                    WheigthPerPortion = 1000,
                    Price = 3
                },
                new Food()
                {
                    Name = "Spaghetti",
                    Description = "No sauce",
                    WheigthPerPortion = 10,
                    Price = 98
                },
            };
                var lines = 4;
                var column = 3;
                var map = new FoodMap(lines, column);
                var random = new Random();
                for (int i = 0; i < lines; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        map.Get(i, j).AvailableQuantity = random.Next(500);
                        var location = new Location(i, j);
                        map.SetQuantity(random.Next(5000), location);
                        var foodChoice = ((i + 1) * (j + 1)) % 6;
                        switch (foodChoice)
                        {
                            case 0:
                                map.AddFood(foods[0], location);
                                break;
                            case 1:
                                map.AddFood(foods[1], location);
                                break;
                            case 2:
                                map.AddFood(foods[2], location);
                                break;
                            case 3:
                                map.AddFood(foods[3], location);
                                break;
                            default:
                                break;
                        }
                    }
                }
                return map;
            }

            /// <inheritdoc/>
            public FoodMap getLatestReadings()
            {
                return MakeReading();
            }
        }
    }
}
