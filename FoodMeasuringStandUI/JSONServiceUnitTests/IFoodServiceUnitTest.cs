using FoodMeasuringObjects.Foods;
using JSONService;
using Services;
using ServiceUnitTests;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace JSONServiceUnitTests
{
    public class IFoodServiceUnitTest : ServiceUnitTest
    {
        [Fact]
        public void RegisterDifferentNameOtherPropertiesRepeatIsNotNull()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();

            // Act
            var food = service.RegisterFood(testName," a", 101, 12);
            var food1 = service.RegisterFood(testName + "1231", " a", 101, 12);

            // Assert
            Assert.Contains(service.GetFoods(), x => x.Name == testName);
            Assert.Contains(service.GetFoods(), x => x.Name == testName + "1231");

            Assert.NotNull(food);
            Assert.NotNull(food1);
        }

        [Fact]
        public void RegisterSameNamemUltipleTimesShouldReturnNull()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();

            // Act
            var food = service.RegisterFood(testName);
            var food1 = service.RegisterFood(testName);

            // Assert
            Assert.Contains(service.GetFoods(), x => x.Name == testName);
            Assert.NotNull(food);
            Assert.Null(food1);
        }

        [Fact]
        public void RegisterNewFoodOnlyNameShouldWork()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();

            // Act
            var food = service.RegisterFood(testName);

            // Assert
            Assert.Contains(service.GetFoods(), x => x.Name == testName);
            Assert.NotNull(food);
        }

        [Fact]
        public void RegisterNewFoodNameAndDescriptionShouldWork()
        {
            // Arrange
            var testName = "Test Register";
            var testDesc = "Test Desc";
            var service = _container.Resolve<IFoodService>();

            // Act
            var food = service.RegisterFood(testName, testDesc);

            // Assert
            Assert.Contains(service.GetFoods(),
                x => x.Name == testName && x.Description == testDesc);
            Assert.NotNull(food);
        }

        [Fact]
        public void RegisterNewFoodNameAndWheigthShouldWork()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();
            var testWheight = 10;

            // Act
            var food = service.RegisterFood(testName,wheigthPerPortion: testWheight);

            // Assert
            Assert.Contains(service.GetFoods(),
                x => x.Name == testName && x.WheigthPerPortion == testWheight);
            Assert.NotNull(food);
        }

        [Fact]
        public void RegisterNewFoodNameAndPriceShouldWork()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();
            var testPrice = 10;

            // Act
            var food = service.RegisterFood(testName, price: testPrice);

            // Assert
            Assert.Contains(service.GetFoods(), 
                x => x.Name == testName && x.Price == testPrice);
            Assert.NotNull(food);
        }

        [Fact]
        public void RegisterNewFoodOnlyNameShoulBeAbleToFindTheSameInstance()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();

            // Act
            var food = service.RegisterFood(testName);

            // Assert
            Assert.NotNull(food);
            Assert.Contains(service.GetFoods(),x => x == food);
        }

        [Fact]
        public void ResetFoodsShouldClearAllFoods()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();
            for(int i = 0; i < 5; i++)
            {
                service.RegisterFood(testName);
            }

            // Act
            service.ResetFoods();

            // Assert
            Assert.Empty(service.GetFoods());
        }

        [Fact]
        public void DeleteFoodShouldDeleteTheCorrectInstanceOfFood()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();
            service.RegisterFood(testName + "qqq123123");
            var food = service.RegisterFood(testName);
            service.RegisterFood(testName+"qqq");


            // Act
            service.DeleteFood(food);

            // Assert
            Assert.Contains(service.GetFoods(), x=> x.Name == testName + "qqq");
        }

        [Fact]
        public void GetShouldFindTheSameInstance()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();
            var food = service.RegisterFood(testName);

            // Act
            var foodGet = service.Get(testName);

            // Assert
            Assert.Equal(food, foodGet);
        }

        [Fact]
        public void GetNotFindAnythingAfterDelete()
        {
            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();
            var food = service.RegisterFood(testName);

            // Act
            var deleted = service.DeleteFood(food);
            var foodGet = service.Get(testName);

            // Assert
            Assert.Null(foodGet);
            Assert.True(deleted);
        }

        [Fact]
        public void UpdateShouldOnlyAffectOneLementByName()
        {

            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();
            var food = service.RegisterFood(testName);

            // Act
            food.Description += "qqq";
            food.WheigthPerPortion = 1000;
            food.Price = 23123;
            var foodGet = service.Update(food);

            // Assert
            Assert.True(foodGet);
            Assert.Contains(service.GetFoods(), x=> x.Name == testName);
            Assert.Contains(service.GetFoods(), x => x.Description == "qqq");
            Assert.Contains(service.GetFoods(), x => x.WheigthPerPortion == 1000);
            Assert.Contains(service.GetFoods(), x => x.Price == 23123);
            Assert.Single(service.GetFoods());
        }

        [Fact]
        public void UpdateShouldNotWorkWHenNameIsWrong()
        {

            // Arrange
            var testName = "Test Register";
            var service = _container.Resolve<IFoodService>();
            var food = service.RegisterFood(testName);
            Assert.NotNull(food);

            // Act
            food.Name += "Diff";
            food.Description += "qqq";
            food.WheigthPerPortion = 1000;
            food.Price = 23123;
            var foodGet = service.Update(food);

            // Assert
            Assert.False(foodGet);
        }

    }
}