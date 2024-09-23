using FoodMeasuringAPI;
using FoodMeasuringObjects.Foods;
using FoodStandUI.ViewModel.Basic;
using FoodMeasuringObjects.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Services;
using System.Windows.Input;

namespace FoodStandUI.ViewModel.Components
{
    internal class FoodViewModel :  BaseViewModel
    {
        Food model;

        public FoodViewModel(Food model)
        {
            this.model = model;
        }

        public void AddToLocation(FoodMeasuringObjects.Telemetry.Location location)
        {
            BackendAPI.Instance.Container.Resolve<ILocalizationService>().AddFood(model, location);
        }

        public ICommand CloseCommand { get; internal set; }

        public ICommand SaveCommand { get; internal set; }


        public string Name
        {
            get
            {
                return model.Name;
            }

            set
            {
                var old = BackendAPI.FoodService.Get(model.Name);
                if (old is not null && BackendAPI.FoodService.UpdateName(old,value))
                {
                    model = old;
                    model.Name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return model.Description;
            }

            set
            {
                Food newModel = model;
                newModel.Description = value;
                if (BackendAPI.FoodService.Update(newModel))
                {
                    model.Description = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int WheigthPerPortion
        {
            get
            {
                return model.WheigthPerPortion;
            }

            set
            {
                Food newModel = model;
                newModel.WheigthPerPortion = value;
                if (BackendAPI.FoodService.Update(newModel))
                {
                    model.WheigthPerPortion = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int Price
        {
            get
            {
                return model.Price;
            }

            set
            {
                Food newModel = model;
                newModel.Price = value;
                if (BackendAPI.FoodService.Update(newModel))
                {
                    model.Price = value;
                    RaisePropertyChanged();
                }
            }
        }

    }
}
