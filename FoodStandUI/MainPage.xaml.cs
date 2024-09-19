using FoodMeasuringAPI;
using FoodStandUI.ViewModel.Components;

namespace FoodStandUI
{
    public partial class MainPage : ContentPage
    {
        MainWindowViewModel VM;
        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            VM = new MainWindowViewModel();
            VM.Width = this.Width;
            VM.Heigth = this.Height;
            BindingContext = VM;
        }
    }

}
