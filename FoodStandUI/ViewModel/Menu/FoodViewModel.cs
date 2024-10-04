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
            // Initial image
            plus = ImageSource.FromFile("Resources\\Images\\minus.jpg");
            // Initial image
            minus = ImageSource.FromFile("Resources\\Images\\plus.jpg");
            ToggleButtonImage = plus;
        }

        private ImageSource plus;
        private ImageSource minus;
        private ImageSource toggleButtonImage;

        public ImageSource ToggleButtonImage
        {
            get => toggleButtonImage;
            set
            {
                if (toggleButtonImage != value)
                {
                    toggleButtonImage = value;
                    RaisePropertyChanged();
                }
            }
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
            if (DetailsVisible)
            {
                ToggleButtonImage = minus;
            }
            else
            {
                ToggleButtonImage = plus;
            }
        }

        internal ICommand ExpandToggleButtonClick { get; private set; } 

    }
}
