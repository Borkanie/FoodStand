using FoodMeasuringObjects.Foods;
using FoodStandUI.ViewModel.Basic;
using System.Windows.Input;

namespace FoodStandUI.ViewModel
{
    internal partial class FoodViewModel : BaseViewModel
    {
        private bool detailsVisible = false;
        private bool showButtons = false;

        private FoodViewModel()
        {
            ExpandToggleButtonClick = new Command(expandToggleButtonClick);
        }

        internal bool DetailsVisible
        {
            get => detailsVisible;
            set
            {
                if (detailsVisible != value)
                {
                    detailsVisible = value;
                    RaisePropertyChanged();
                }
            }
        }

        internal bool ShowButtons
        {
            get => showButtons;
            set
            {
                if (showButtons != value)
                {
                    showButtons = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void expandToggleButtonClick()
        {
            DetailsVisible = !DetailsVisible;
        }

        internal ICommand ExpandToggleButtonClick { get; private set; } 

    }
}
