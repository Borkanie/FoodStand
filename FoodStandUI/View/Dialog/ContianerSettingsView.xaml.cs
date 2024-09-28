using CommunityToolkit.Mvvm.Messaging;
using FoodStandUI.Resources;
using FoodStandUI.Resources.Messages;
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

    internal void SaveButton_Clicked(object sender, EventArgs e)
    {
		if(BindingContext is not null)
		{
		((ContainerViewModel)BindingContext).UpdateModel();
			WeakReferenceMessenger.Default.Send(new ContainerSettingsViewMessage(this, ContainerSettingsViewMessage.Action.SaveSettings));
        }
    }
}