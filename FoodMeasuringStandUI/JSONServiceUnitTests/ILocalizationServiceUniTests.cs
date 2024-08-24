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
