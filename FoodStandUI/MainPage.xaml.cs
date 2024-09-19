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
            SizeChanged += MainPage_SizeChanged;
        }

        private void MainPage_SizeChanged(object? sender, EventArgs e)
        {
            if(Width != -1 && Height != -1 && VM is not null)
            {
                VM.Heigth = Height;
                VM.Width = Width;
            }
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            VM = new MainWindowViewModel();
            VM.Width = Width;
            VM.Heigth = Height;
            BindingContext = VM;
        }
    }

}
