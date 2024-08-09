using FoodMeasuringAPI;
using FoodMeasuringObjects.Foods;
using FoodStandUI.ViewModel.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.ViewModel.Components
{
    internal class FoodViewModel :  BaseViewModel
    {
        Food model;

        public string Name
        {
            get
            {
                return model.Name;
            }

            set
            {
                Food newModel = model;
                newModel.Name = value;
                if (BackendAPI.Update(newModel))
                {
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
                if (BackendAPI.Update(newModel))
                {
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
                if (BackendAPI.Update(newModel))
                {
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
                if (BackendAPI.Update(newModel))
                {
                    RaisePropertyChanged();
                }
            }
        }

    }
}
