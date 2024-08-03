namespace FoodMeasuringObjects.Telemetry
{
    public class Location
    {
        public Location()
        {
            
        }

        public Location(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; set; } = 0;

        public int Column { get; set; } = 0;
    }
}
