using FoodMeasuringObjects.Foods;
using FoodStandUI.ViewModel.Components;

namespace FoodStandUI.View;

public partial class FoodListView : ContentView
{
	public FoodListView()
	{
		InitializeComponent();
	}

	internal FoodViewModel ViewModel { get; set; }

}