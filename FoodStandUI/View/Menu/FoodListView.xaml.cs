using CommunityToolkit.Mvvm.Messaging;
using FoodStandUI.Resources.Messages;
using FoodStandUI.Services;
using FoodStandUI.ViewModel.Components;
using FoodStandUI.ViewModel;

namespace FoodStandUI.View.Menu;

public partial class FoodListView : ContentView
{
	public FoodListView()
	{
		InitializeComponent();
	}

    internal void CancelButton_Clicked(object sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new FoodMessage(this, FoodMessage.Action.CancelSettings));
    }

    internal void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is not null)
        {
            var vm = ((FoodViewModel)BindingContext);
            if (ParseInputs(vm) && vm.UpdateModel())
            {
                WeakReferenceMessenger.Default.Send(new FoodMessage(this, FoodMessage.Action.CancelSettings));
            }
            else
            {
                AlertService.Instance.ShowAlert("Error", "Couldn't save model to the dbb.");
            }
        }
    }

    private bool ParseInputs(FoodViewModel vm)
    {
        int wheigth = 0;
        int price = 0;
        /*
         *  if (int.TryParse(PriceText.Text, out price) &&
            int.TryParse(WheigthPerPortionText.Text, out wheigth))
        {
            vm.Name = NameE.Text;
            vm.Description = DescriptionText.Text;
            vm.Price = price;
            vm.WheigthPerPortion = wheigth;
            return true;
        }
         */
        return false;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Skip first population
        if (IsLoaded)
        {
            if(BindingContext != null)
            {
                var vm = (FoodViewModel)BindingContext;
                vm.ShowButtons = true;
            }
        }
    }
}