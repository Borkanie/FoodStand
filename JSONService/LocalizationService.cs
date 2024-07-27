using FoodMeasuringObjects.Telemetry;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONService
{
    internal class LocalizationService : ILocalizationService
    {

        private IFoodService _foodService;
        private FoodMap _foodMap;
        public LocalizationService(IFoodService foodService)
        {
            _foodService = foodService;
        }

        private List<Location> GetUsedLocations()
        {
            var locations = new List<Location>();

            return locations;
        }

        public FoodMap GetFoodMap()
        {
            if(_foodMap is null)
                ResetFoodMap();
            return _foodMap;
        }

        public void ResetFoodMap()
        {
            FoodMapBuilder builder = FoodMapBuilder.Instance;
            var locations = GetUsedLocations();
            int index = 0;
            foreach (var food in _foodService.GetFoods())
            {
                builder.AddFood(food, locations[index]);
                index = (index + 1) % locations.Count;
            }
            _foodMap = builder.Finish();

        }

        public bool Update(Location location)
        {
            var oldItem = _foodMap.Items.First(x => x.Location.Contains(location));
            if(oldItem != null)
            {
                oldItem.Location.Add(location);
                return true;

            }
            return false;
        }
    }
}
