using FoodMeasuringObjects.Orders;

namespace FoodMeasuringObjects.Telemetry
{
    public class FoodMap
    {
        public Item? getFromIndex(int line, int column)
        {
            if (line > ElementsOnLine || column > ElementsOnColumn)
                return null;
            return Items.FirstOrDefault(x => x.Location.Line == line && x.Location.Column == column);
        }

        public int ElementsOnLine { get; set; } = 0;

        public int ElementsOnColumn { get; set; } = 0;

        List<Item> Items { get; set; } = new List<Item> { };
    }
}
