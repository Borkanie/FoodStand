using CommunityToolkit.Mvvm.Messaging;
using FoodStandUI.Resources;
using FoodStandUI.Resources.Messages;
using FoodStandUI.Services;
using FoodStandUI.ViewModel.Components;

namespace FoodStandUI.View.Dialog;

public partial class ContianerSettingsView : ContentView
{
	public ContianerSettingsView()
	{
		InitializeComponent();
	}

    internal void SetViewModel(ContainerViewModel vm)
	{
		BindingContext = vm;
		FoodSettings.BindingContext = vm.Food;
	}

	internal void CancelButton_Clicked(object sender, EventArgs e)
	{
        WeakReferenceMessenger.Default.Send(new ContainerSettingsViewMessage(this, ContainerSettingsViewMessage.Action.CancelSettings));
	}

	private bool ParseInputs(ContainerViewModel vm)
	{
		int wheigth = 0;
		int price = 0;
		int line = 0;
		int column = 0;
		if(int.TryParse(ColumnText.Text,out column) &&
            int.TryParse(PriceText.Text, out price) &&
            int.TryParse(LineText.Text, out line) &&
            int.TryParse(WheigthPerPortionText.Text, out wheigth))
		{
			vm.Food.Name = NameText.Text;
			vm.Food.Description = DescriptionText.Text;
			vm.Food.Price = price;
			vm.Food.WheigthPerPortion = wheigth;
			vm.Line = line;
			vm.Column = column;
			return true;
        }
		return false;
	}

    internal void SaveButton_Clicked(object sender, EventArgs e)
    {
		if(BindingContext is not null)
		{
			var vm = ((ContainerViewModel)BindingContext);
            if (ParseInputs(vm) && vm.UpdateModel())
			{
                WeakReferenceMessenger.Default.Send(new ContainerSettingsViewMessage(this, ContainerSettingsViewMessage.Action.CancelSettings));
            }
            else
			{
				AlertService.Instance.ShowAlert("Error", "Couldn't save model to the dbb.");
            }
		}
    }
}