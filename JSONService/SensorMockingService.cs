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
            var lines = 4;
            var column = 3;
            var map = new FoodMap(lines, column);
            var random = new Random();
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    var item = new Item();
                    var location = new Location(i, j);
                    item.Quantity = random.Next(5000);

                    map.SetItem(item, location);
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
