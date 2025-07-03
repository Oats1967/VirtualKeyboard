using Controls;

namespace VirtualKeyboard.Controls;

public partial class GermanKeyboard : ContentView
{
	public GermanKeyboard(KeyboardViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}