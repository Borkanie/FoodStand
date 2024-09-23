using FoodStandUI.Resources;
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
		MessagingCenter.Send(this, MessageType.CloseContainerSettingsView.Value);
	}

    internal void SaveButton_Clicked(object sender, EventArgs e)
    {
		if(BindingContext is not null)
		{
		((ContainerViewModel)BindingContext).UpdateModel();
        MessagingCenter.Send(this, MessageType.SaveContainerSettingsView.Value);
		}
    }
}