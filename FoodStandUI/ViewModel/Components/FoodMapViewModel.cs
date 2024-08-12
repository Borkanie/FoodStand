using FoodMeasuringAPI;
using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.ViewModel.Components
{
    internal class FoodMapViewModel
    {
        FoodMap model;

        public FoodMapViewModel(FoodMap model)
        {
            this.model = model;
        }

        public ItemViewModel Get(int line, int column)
        {
            return new ItemViewModel(model.Get(line, column));
        }

        public ItemViewModel Get(FoodMeasuringObjects.Telemetry.Location location)
        {
            if (model.Get(location) != null)
                return new ItemViewModel(model.Get(location)!);
            else
                return new ItemViewModel();
        }

        public int ElementsOnLine()
        {
            return model.ElementsOnLine;
        }

        public int ElementsOnColumn()
        {
            return model.ElementsOnColumn;
        }

        public void AddFood(Food food, FoodMeasuringObjects.Telemetry.Location targetLocation)
        {
            model.AddFood(food, targetLocation);
        }

        public List<ItemViewModel> GetitemList() 
        {
            var list = new List<ItemViewModel>();

            foreach (var item in model.GetItemList())
            {
                list.Add(new ItemViewModel(item));
            }
            return list;
        }

        public void SetItem(Item item, FoodMeasuringObjects.Telemetry.Location location)
        {
            model.SetItem(item, location);
        }

    }
}
