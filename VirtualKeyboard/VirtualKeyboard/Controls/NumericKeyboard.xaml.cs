

using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Controls;

public partial class NumericKeyboard : Grid
{
	public NumericKeyboard(MainPageViewModel viewModel)
	{
		BindingContext =  viewModel;
		InitializeComponent();
       
	}

   
}