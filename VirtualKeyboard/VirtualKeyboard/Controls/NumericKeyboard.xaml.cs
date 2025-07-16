

using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Controls;

public partial class NumericKeyboard : ContentView
{
	public NumericKeyboard(MainPageViewModel viewModel)
	{
		BindingContext =  viewModel;
		InitializeComponent();
        //Loaded += NumericKeyboard_Loaded;
	}

   
}