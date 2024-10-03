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
        private string name;

        public FoodViewModel(Food model)
        {
            this.model = model;
            name = model.Name;
        }

        public bool AddToLocation(FoodMeasuringObjects.Telemetry.Location location)
        {
            return BackendAPI.Instance.Container.Resolve<ILocalizationService>().AddFood(model, location);
        }
        
        public bool UpdateModel()
        {
            if(BackendAPI.FoodService.UpdateName(model,name))
            {
                model.Name = name;
                return BackendAPI.FoodService.Update(model);
            }
            return false;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != name)
                {
                    name = value;
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
                if (value != model.Description)
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
                if(value != model.WheigthPerPortion)
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
                if(value != model.Price)
                {
                    model.Price = value;
                    RaisePropertyChanged();   
                }
            }
        }

    }
}
