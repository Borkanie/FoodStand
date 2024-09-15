using FoodMeasuringObjects.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMeasuringObjects.Orders
{
    public class Item
    {
        public Guid Id { get; set; }

        public Item(Food food)
        {
            Food = food;
            Id = Guid.NewGuid();
        }

        public Food Food { get; set; } 

        public int Cost
        {
            get
            {
                if (Food is null)
                    return 0;
                return Food.Price * Quantity;
            }
        }

        public int Quantity { get; set; } = 0;

        public override bool Equals(object? obj)
        {
            if (obj is not Item || obj == null)
            {
                return false;
            }
            Item item = (Item)obj;
            if (item.Food is not null)
                return Food is not null && item.Food! == Food!;
            else
                return Food is null;
        }

        public static bool operator ==(Item left, Item right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Item left, Item right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
