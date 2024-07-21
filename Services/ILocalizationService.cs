using FoodMeasuringObjects.Telemetry;

namespace Services
{
    public interface ILocalizationService
    {
        public FoodMap GetFoodMap();

        public void ResetFoodMap();

        public bool Update(Location location);
    }
}
