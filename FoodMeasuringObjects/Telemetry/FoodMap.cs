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
            for(int i=0; i < numberOfLines; i++)
            {
                for(int j=0;j < numberOfColumns; j++)
                {
                    itemTable[i,j] = new Item();
                }
            }
        }

        public Item Get(int line, int column)
        {
            if (itemTable == null || line > ElementsOnLine || column > ElementsOnColumn)
                throw new IndexOutOfRangeException();
            
            return itemTable[line,column];
        }

        public Item Get(Location location)
        {
            if (itemTable == null || location.Line > ElementsOnLine
                || location.Column > ElementsOnColumn)
                throw new IndexOutOfRangeException();
            
            return itemTable[location.Line,location.Column];
        }


        public int ElementsOnLine 
        { 
            get {
                return itemTable.GetLength(0);
            } 
        }

        public int ElementsOnColumn 
        {
            get
            {
                if(itemTable.Length > 0)
                    return itemTable.GetLength(1);
                return 0;
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
                if(item is not null)
                    list.Add(item);
            }
            return list;
        }

        public void SetQuantity(int quantity, Location location)
        {
            if (LocationIsCorrect(location))
            {
                itemTable[location.Line, location.Column].Quantity = quantity;
            }
        }

        private bool LocationIsCorrect(Location location)
        {
            if(ElementsOnLine == 0) 
                return false;
            return location.Line < ElementsOnLine && location.Column < ElementsOnColumn;
        }

        /// <inheritdoc/>
        public List<Location> AskForLocation(Food food)
        {
            List<Location> locations = new List<Location>();
            for (int i = 0; i < ElementsOnLine; i++)
            {
                for (int j = 0; j < ElementsOnColumn; j++)
                {
                    if (Get(i, j) is not null && Get(i, j).Food == food)
                    {
                        locations.Add(new Location() { Line = i, Column = j });
                    }
                }
            }
            return locations;
        }

        public override bool Equals(object? obj)
        {
            if(obj is not FoodMap || obj == null)
            {
                return false;
            }
            var map = (FoodMap)obj;
            if(map.ElementsOnLine != ElementsOnLine ||
                map.ElementsOnColumn != ElementsOnColumn)
                return false;
            var myElements = GetItemList();
            var mapElements = map.GetItemList();
            if (myElements.Count != mapElements.Count)
                return false;
            for(int i=0;i<myElements.Count; i++)
            {
                if (myElements[i] != mapElements[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator ==(FoodMap left, FoodMap right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(FoodMap left, FoodMap right)
        {
            return !left.Equals(right);
        }

        public FoodMap Clone()
        {
            var map = new FoodMap(ElementsOnLine, ElementsOnColumn);
            for(int i = 0; i < ElementsOnLine; i++)
            {
                for (int j = 0; j < ElementsOnColumn; j++)
                {
                    map.itemTable[i, j] = new Item()
                    {
                        Food = itemTable[i, j].Food,
                        Id = itemTable[i, j].Id,
                        Quantity = itemTable[i, j].Quantity,
                        Type = itemTable[i, j].Type,
                    };
                }
            }

            return map;
        }
    }
}
