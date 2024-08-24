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

        public override bool Equals(object? obj)
        {
            if(obj is null || obj is not Location) 
                return false;
            var loc = (Location)obj;
            return loc.Line == Line && loc.Column == Column;    
        }
    }
}
