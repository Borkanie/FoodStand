﻿using Autofac.Core;
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
        
        public FoodMapViewModel()
        {

        }

        public FoodMap Model
        {
            get => BackendAPI.Instance.Container.Resolve<ILocalizationService>().GetFoodMap();
        }

        public ContainerViewModel Get(int line, int column)
        {
            var item = Model.Get(line, column);
            if(item is null)
                return new ContainerViewModel();
            else
                return new ContainerViewModel(item);
        }

        public ContainerViewModel Get(FoodMeasuringObjects.Telemetry.Location location)
        {
            if (Model.Get(location) is not null)
                return new ContainerViewModel(Model.Get(location));
            else
                return new ContainerViewModel();
        }

        public int ElementsOnLine()
        {
            return Model.ElementsOnLine;
        }

        public int ElementsOnColumn()
        {
            return Model.ElementsOnColumn;
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
