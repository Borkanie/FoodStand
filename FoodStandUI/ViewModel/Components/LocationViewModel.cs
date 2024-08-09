using FoodStandUI.ViewModel.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.ViewModel.Components
{
    internal class LocationViewModel : BaseViewModel
    {
        FoodMeasuringObjects.Telemetry.Location model;

        public LocationViewModel()
        {
            
        }

        public LocationViewModel(FoodMeasuringObjects.Telemetry.Location model)
        {
            this.model = model;
        }


        public int Line
        {
            get
            {
                return model.Line;
            }
            set
            {
                if(model.Line != value)
                {
                    model.Line = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int Column
        {
            get
            {
                return model.Column;
            }
            set
            {
                if(model.Column != value)
                {
                    model.Column = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
