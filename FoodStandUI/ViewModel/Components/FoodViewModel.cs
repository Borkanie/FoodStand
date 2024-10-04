using FoodMeasuringAPI;
using FoodMeasuringObjects.Foods;
using FoodStandUI.ViewModel.Basic;

namespace FoodStandUI.ViewModel
{
    internal partial class FoodViewModel :  BaseViewModel
    {
        private Food model;
        private string name;

        public FoodViewModel(Food model)
        {
            this.model = model;
            name = model.Name;
        }

        public bool AddToLocation(FoodMeasuringObjects.Telemetry.Location location)
        {
            return BackendAPI.LocalizationService.AddFood(model, location);
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
