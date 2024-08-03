using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;

namespace FoodMeasuringObjects.Telemetry
{
    public class FoodMap
    {
        private Item[,] itemTable { get; set; } 
        
        public FoodMap(int numberOfLines, int numberOfColumns)
        {
            itemTable = new Item[numberOfLines, numberOfColumns];    
        }

        public Item? Get(int line, int column)
        {
            if (itemTable == null || line > ElementsOnLine || column > ElementsOnColumn)
                return null;
            
            return itemTable[line,column];
        }

        public Item? Get(Location location)
        {
            if (itemTable == null || location.Line > ElementsOnLine 
                || location.Column > ElementsOnColumn)
                return null;
            
            return itemTable[location.Line,location.Column];
        }


        public int ElementsOnLine 
        { 
            get {
                if(itemTable.Length > 0)
                    return itemTable.GetLength(1);
                return 0;
            } 
        }

        public int ElementsOnColumn 
        {
            get
            {
                return itemTable.GetLength(0);
            }    
        }

        public void AddFood(Food food, Location targetLocation)
        {
            var targetItem = Get(targetLocation);
            targetItem.Food = food;
        }

        public List<Item> GetItemList()
        {
            var list = new List<Item>();

            foreach (var item in itemTable)
            {
                list.Add(item);
            }
            return list;
        }

        public void SetItem(Item item, Location location)
        {
            if (LocationIsCorrect(location))
            {
                itemTable[location.Line, location.Column] = item;
            }
        }

        private bool LocationIsCorrect(Location location)
        {
            if(ElementsOnLine == 0) 
                return false;
            return location.Line < ElementsOnLine && location.Column < ElementsOnColumn;
        }
    }
}
