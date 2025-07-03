using Controls;

namespace VirtualKeyboard.Controls;

public partial class DutchKeyboard : ContentView
{
	public DutchKeyboard(KeyboardViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}