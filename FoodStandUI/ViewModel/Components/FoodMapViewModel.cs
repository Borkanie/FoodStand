using FoodMeasuringAPI;
using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;
using FoodStandUI.ViewModel.Basic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.ViewModel.Components
{
    internal class FoodMapViewModel : BaseViewModel
    {
        FoodMap model;
        
        public FoodMap Model
        {
            get => model;
            set
            {
                if(model != value)
                {
                    model = value;
                    ItemList.Clear();
                    foreach (var item in model.GetItemList())
                    {
                        ItemList.Add(new ItemViewModel(item));
                    }
                    RaisePropertyChanged(nameof(Model));
                }
            }
        }
        
        public FoodMapViewModel()
        {
            
        }


        public FoodMapViewModel(FoodMap model)
        {
            Model = model;            
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

        public ObservableCollection<ItemViewModel> ItemList { get; } = new ObservableCollection<ItemViewModel>();

        public void SetItem(Item item, FoodMeasuringObjects.Telemetry.Location location)
        {
            model.SetItem(item, location);
        }

    }
}
