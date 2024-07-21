using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Telemetry;

namespace FoodMeasuringObjects.Orders
{
    public class Item
    {
        public int Cost
        {
            get
            {
                return Food.Price * Quantity;
            }
        }

        public Guid Id { get; set; }

        public Food Food { get; set; }

        private int _quantity = 0;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity += value / Food.WheigthPerPortion;
            }
        }

        public FoodPortioningType Type { get; set; }

        public int AvailableQuantity()
        {
            return Food.Wheigth / Food.WheigthPerPortion;
        }

        public Location Location { get; set; }
    }
}
