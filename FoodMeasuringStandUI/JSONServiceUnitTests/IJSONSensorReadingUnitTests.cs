using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;
using JSONService;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONServiceUnitTests
{
    public class IJSONSensorReadingUnitTests
    {
        private static FoodMap getDefaultFoodMap()
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
                    var location = new Location(i, j);
                    map.SetQuantity(random.Next(5000), location);
                    var foodChoice = ((i + 1) * (j + 1 ))% 6;
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


        [Fact]
        public void NewServiceReturnsDefaultValueCorrectly()
        {
            // Arrange
            var sensorReadingService = new SensorMockingService();
            var defaultFoodMap = getDefaultFoodMap();

            // Act
            var readFoodMap = sensorReadingService.getLatestReadings();

            // Assert
            Assert.Equal(defaultFoodMap, readFoodMap);
        }

        [Fact]
        public void MultuipleInstancesOfTheSameServiceReturnSameResult()
        {
            // Arrange
            var sensorReadingService = new SensorMockingService();
            var sensorReadingService1 = new SensorMockingService();

            // Act
            var readFoodMap = sensorReadingService.getLatestReadings();
            var readFoodMap1 = sensorReadingService1.getLatestReadings();

            // Assert
            Assert.Equal(readFoodMap1, readFoodMap);
        }

        [Fact]
        public void MultipleReadingsReturnTheSameResult()
        {
            // Arrange
            var sensorReadingService = new SensorMockingService();

            // Act
            var readings = new[]
            {
                sensorReadingService.getLatestReadings(),
                sensorReadingService.getLatestReadings(),
                sensorReadingService.getLatestReadings(),
                sensorReadingService.getLatestReadings(),
            };

            // Assert
            var expected = getDefaultFoodMap();
            Assert.All(readings, reading =>
            {
                Assert.Equal(reading, expected);
            });
        }
    }
}
