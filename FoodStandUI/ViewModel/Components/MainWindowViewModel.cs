using FoodMeasuringAPI;
using FoodStandUI.ViewModel.Basic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodStandUI.ViewModel.Components
{
    internal class MainWindowViewModel : BaseViewModel
    {

        internal class OverlayEnumToVisibilityConverter : IValueConverter
        {
            #region Helpers
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                // Ensure the value is of type enum
                if (value is Overlays enumValue && parameter is string targetValue)
                {
                    // Compare the enum value with the parameter
                    return enumValue.ToString() == targetValue ? true : false;
                }
                return false;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        internal enum Overlays
        {
            NONE,
            FoodList,
        }
        #endregion

        private Overlays currentOverlay = Overlays.NONE;

        public MainWindowViewModel()
        {
            foreach(var food in BackendAPI.FoodService.GetFoods())
            {
                FoodViewModels.Add(new FoodViewModel(food));
            }
        }

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

        public Overlays CurrentOverlay
        {
            get => currentOverlay;
            set
            {
                if(currentOverlay != value)
                {
                    currentOverlay = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<FoodViewModel> FoodViewModels = new();

        public FoodMapViewModel FoodMap{  get; set;  } = new FoodMapViewModel();
    }
}
