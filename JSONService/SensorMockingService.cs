using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;
using Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JSONService
{
    /// <inheritdoc/>
    public class SensorMockingService : ISensorReadingService
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
