using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;

namespace FoodMeasuringObjects.Telemetry
{
    public class FoodMap
    {
        public FoodMap(int numberOfLines, int numberOfColumns)
        {
            for(int i = 0; i < numberOfLines; i++)
            {
                ItemTable.Add(new List<Item>());
                for(int j=0;j< numberOfColumns; j++)
                {
                    Location currentLocation = new Location() { Column = j, Line = i };
                    ItemTable[i].Add(new Item(new List<Location>() { currentLocation }));
                }
            }    
        }

        public Item? Get(int line, int column)
        {
            if (line > ElementsOnLine || column > ElementsOnColumn)
                return null;
            var searchLocation = new Location()
            {
                Column = column,
                Line = line
            };
            foreach(var itemLine in ItemTable)
            {
                foreach(var item in itemLine)
                {
                    if (item.Location.Contains(searchLocation))
                        return item;
                }
            }
            return null;
        }

        public Item? Get(Location location)
        {
            if (location.Line > ElementsOnLine || location.Column > ElementsOnColumn)
                return null;
            
            foreach (var itemLine in ItemTable)
            {
                foreach (var item in itemLine)
                {
                    if (item.Location.Contains(location))
                        return item;
                }
            }
            return null;
        }

        public List<List<Item>> ItemTable { get; set; } = new();

        public int ElementsOnLine 
        { 
            get {
                if(ItemTable.Count > 0)
                    return ItemTable[0].Count;
                return 0;
            } 
        }

        public int ElementsOnColumn 
        {
            get
            {
                return ItemTable.Count;
            }    
        }

        public void AddFood(Food food, Location targetLocation)
        {
            var targetItem = Get(targetLocation);
            if (targetItem != null)
            {
                throw new InvalidOperationException("Location is already in use");
            }
            targetItem.Food = food;
            targetItem.Location.Add(targetLocation);
        }

        public List<Item> GetItemList()
        {
            var list = new List<Item>();


            foreach (var itemLine in ItemTable)
            {
                foreach (var item in itemLine)
                {
                    if (!list.Contains(item))
                        list.Add(item);
                       
                }
            }
            return list;
        }
    }
}
