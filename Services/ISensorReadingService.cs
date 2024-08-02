using FoodMeasuringObjects.Telemetry;

namespace Services
{
    /// <summary>
    /// Returns the configuration identified from the external hardware.
    /// </summary>
    public  interface ISensorReadingService
    {
        /// <summary>
        /// Checks for all the available items in the sensor and returns a mapping of all the elemnts.
        /// Some idnexes might not exist in the mapping this means that palce is empty.
        /// </summary>
        /// <returns>A map of all the read contianers.</returns>
        FoodMap getLatestReadings();
    }
}
