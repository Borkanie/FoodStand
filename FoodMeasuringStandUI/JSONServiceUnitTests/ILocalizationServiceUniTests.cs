using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Telemetry;
using Services;
using ServiceUnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JSONServiceUnitTests
{
    public class ILocalizationServiceUniTests : ServiceUnitTest
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


        [Fact]
        public void NewServiceReturnsDefaultValueCorrectly()
        {
            // Arrange
            var sensorReadingService = _container.Resolve<ILocalizationService>();
            var defaultFoodMap = getDefaultFoodMap();

            // Act
            var readFoodMap = sensorReadingService.GetFoodMap();

            // Assert
            Assert.Equal(defaultFoodMap, readFoodMap);
        }

        [Fact]
        public void MultuipleInstancesOfTheSameServiceReturnSameResult()
        {
            // Arrange
            var sensorReadingService = _container.Resolve<ILocalizationService>();
            var sensorReadingService1 = _container.Resolve<ILocalizationService>();

            // Act
            var readFoodMap = sensorReadingService.GetFoodMap();
            var readFoodMap1 = sensorReadingService1.GetFoodMap();

            // Assert
            Assert.Equal(readFoodMap1, readFoodMap);
        }

        [Fact]
        public void MultipleReadingsReturnTheSameResult()
        {
            // Arrange
            var sensorReadingService = _container.Resolve<ILocalizationService>();

            // Act
            var readings = new[]
            {
                sensorReadingService.GetFoodMap(),
                sensorReadingService.GetFoodMap(),
                sensorReadingService.GetFoodMap(),
                sensorReadingService.GetFoodMap(),
            };

            // Assert
            var expected = getDefaultFoodMap();
            Assert.All(readings, reading =>
            {
                Assert.Equal(reading, expected);
            });
        }

        [Fact]
        public void GetFoodMapReturnsTheSameResultIfNoChangesAreDone()
        {
            // Arrange
            var foodMap = _container.Resolve<ILocalizationService>().GetFoodMap();

            // Act
            var newMap = _container.Resolve<ILocalizationService>().GetFoodMap();

            // Assert
            Assert.Equal(foodMap, newMap);
        }

        [Fact]
        public void AddFoodAddsItInAnotherLocation()
        {
            // Arrange
            var foodMapService = _container.Resolve<ILocalizationService>();
            var firstItem = foodMapService.GetItemList().First();
            var locaitons = foodMapService.AskForLocation(firstItem.Food);
            Location newLocation = new Location(0, 0);
            for(int i=0;i<foodMapService.GetFoodMap().ElementsOnLine; i++)
            {
                for(int j=0;j < foodMapService.GetFoodMap().ElementsOnColumn; j++)
                {
                    newLocation = new Location(i, j);
                    if (!locaitons.Contains(newLocation))
                    {
                        break;
                    }
                }
            }

            // Act
            foodMapService.AddFood(firstItem.Food, newLocation);

            // Assert
            Assert.True(foodMapService.AskForLocation(firstItem.Food).Count > locaitons.Count);
            Assert.Contains(newLocation, foodMapService.AskForLocation(firstItem.Food));
        }

        [Fact]
        public void AddFoodInUsedLocationOverridesFoodAtThatLocation()
        {
            // Arrange
            var foodMapService = _container.Resolve<ILocalizationService>();
            var items = foodMapService.GetItemList();
            var firstItem = items.First();
            Assert.NotNull(firstItem.Food);
            var secondItem = items[1];
            foreach(var item in items)
            {
                if(item.Food is not null &&
                    item.Food != firstItem.Food!)
                {
                    secondItem = item;
                    break;
                }
            }
            
            // Act
            var newLocaiton = foodMapService.AskForLocation(secondItem.Food!).First();
            foodMapService.AddFood(firstItem.Food!, newLocaiton);
            
            // Assert
            Assert.Contains(newLocaiton, foodMapService.AskForLocation(firstItem.Food!));
            Assert.DoesNotContain(newLocaiton, foodMapService.AskForLocation(secondItem.Food!));
        }

        [Fact]
        public void RecordChangesisNotNullWheQuantityGetsRandomlyChoosed()
        {
            // Arrange
            var foodMapService = _container.Resolve<ILocalizationService>();

            // Act
            var changes = foodMapService.GetFoodChanges();

            // Assert
            Assert.NotEmpty(changes);
        }
    }
}
