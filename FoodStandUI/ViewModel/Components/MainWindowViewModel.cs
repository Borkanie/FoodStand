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
        public FoodMapViewModel FoodMap
        {
            get;
            set;
        }
    }
}
