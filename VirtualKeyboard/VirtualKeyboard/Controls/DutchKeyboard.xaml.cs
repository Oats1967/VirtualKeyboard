

using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Controls;

public partial class DutchKeyboard : Grid
{
	public DutchKeyboard(MainPageViewModel viewModel)
	{
		BindingContext = viewModel;
		
		InitializeComponent();
      
	}

   
}