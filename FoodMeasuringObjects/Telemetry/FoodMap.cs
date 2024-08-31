using FoodMeasuringObjects.Foods;

namespace FoodMeasuringObjects.Telemetry
{
    public class FoodMap
    {
        private FoodContainer[,] itemTable { get; set; } 
        
        public FoodMap(int numberOfLines, int numberOfColumns)
        {
            itemTable = new FoodContainer[numberOfLines, numberOfColumns];
            for(int i=0; i < numberOfLines; i++)
            {
                for(int j=0;j < numberOfColumns; j++)
                {
                    itemTable[i,j] = new FoodContainer(new Location() { Line = i, Column = j});
                }
            }
        }

        public FoodContainer Get(int line, int column)
        {
            if (itemTable == null || line > ElementsOnLine || column > ElementsOnColumn)
                throw new IndexOutOfRangeException();
            
            return itemTable[line,column];
        }

        public FoodContainer Get(Location location)
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

        public List<FoodContainer> GetItemList()
        {
            var list = new List<FoodContainer>();

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
                itemTable[location.Line, location.Column].AvailableQuantity = quantity;
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
                    map.itemTable[i, j] = new FoodContainer(new Location() { Line = i, Column = j })
                    {
                        Food = itemTable[i, j].Food,
                        Id = itemTable[i, j].Id,
                        AvailableQuantity = itemTable[i, j].AvailableQuantity,
                        Type = itemTable[i, j].Type,
                    };
                }
            }

            return map;
        }
    }
}
