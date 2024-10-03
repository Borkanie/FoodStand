using Autofac.Core;
using FoodMeasuringAPI;
using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodMeasuringObjects.Telemetry;
using FoodStandUI.ViewModel.Basic;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace FoodStandUI.ViewModel.Components
{
    internal class FoodMapViewModel : BaseViewModel
    {
        private FoodMap model;
        public FoodMapViewModel()
        {
            model = BackendAPI.Instance.Container.Resolve<ILocalizationService>().GetFoodMap();
            foreach(var item in model.GetItemList())
            {
                ItemList.Add(new ContainerViewModel(item));
            }
        }

        public int ElementsOnLine
        {
            get => model.ElementsOnLine;
        }

        public override double Heigth
        {
            get => base.Heigth;
            set
            {
                if (base.Heigth != value)
                {
                    base.Heigth = value;
                    foreach (var item in ItemList)
                    {
                        item.Heigth = value / model.ElementsOnColumn;
                    }
                }
            }
        }

        public override double Width
        {
            get => base.Width;
            set
            {
                if (base.Width != value)
                {
                    base.Width = value;
                    foreach(var item in ItemList)
                    {
                        item.Width = value/model.ElementsOnLine;
                    }
                }
            }
        }

        public FoodMap Model
        {
            get
            {
                var changes = BackendAPI.Instance.Container.Resolve<ILocalizationService>().GetFoodChanges();
                if(changes.Keys.Count != 0)
                {
                    var items = model.GetItemList();
                    foreach(var item in items)
                    {
                        if (changes.ContainsKey(item))
                        {
                            item.AvailableQuantity += changes[item];
                        }
                    }
                    RaisePropertyChanged();
                }
                return model;
            }
        }

        public ContainerViewModel Get(int line, int column)
        {
            return new ContainerViewModel(Model.Get(line, column));
        }

        public ContainerViewModel Get(FoodMeasuringObjects.Telemetry.Location location)
        {
            return new ContainerViewModel(Model.Get(location));
        }

        public void AddFood(Food food, FoodMeasuringObjects.Telemetry.Location targetLocation)
        {
            Model.AddFood(food, targetLocation);
        }

        public ObservableCollection<ContainerViewModel> ItemList { get; } = new ObservableCollection<ContainerViewModel>();

        public void SetItem(Food food, FoodMeasuringObjects.Telemetry.Location location)
        {
            BackendAPI.Instance.Container.Resolve<ILocalizationService>().AddFood(food, location);
        }

    }
}
