using FoodStandUI.ViewModel.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStandUI.ViewModel.Components
{
    internal class MainWindowViewModel : BaseViewModel
    {
        public override double Heigth 
        { 
            get => base.Heigth;
            set
            {
                if (base.Heigth != value) {
                    base.Heigth = value;
                    FoodMap.Heigth = value;
                    RaisePropertyChanged();
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
                    FoodMap.Width = value;
                    RaisePropertyChanged();
                }
            }
        }

        public FoodMapViewModel FoodMap{  get; set;  } = new FoodMapViewModel();
    }
}
