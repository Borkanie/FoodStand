using FoodMeasuringAPI;
using FoodMeasuringObjects.Foods;
using FoodMeasuringObjects.Orders;
using FoodStandUI.ViewModel.Basic;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace FoodStandUI.ViewModel.Components
{
    internal class ContainerViewModel : BaseViewModel
    {
        FoodContainer model;

        public ContainerViewModel(FoodContainer model)
        {
            Model = model;
        }
        public FoodContainer Model
        {
            get => model;
            set
            {
                if(model is null || model != value)
                {
                    model = value;
                    RaisePropertyChanged(nameof(Model));
                }
            }
        }

        public Food Food
        {
            get
            {
                return model.Food;
            }
            set
            {
                if (model.Food != value)
                {
                    model.Food = value;
                    BackendAPI.Instance.Container.Resolve<ILocalizationService>().AddFood(value, model.Location);
                    this.RaisePropertyChanged();
                }
            }
        }

        public int Quantity
        {
            get
            {
                return model.AvailableQuantity;
            }
        }

        public FoodPortioningType Type
        {
            get
            {
                return model.Type;
            }
            set
            {
                if (model.Type != value)
                {
                    model.Type = value;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}
