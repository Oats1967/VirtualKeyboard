using Controls;

namespace VirtualKeyboard.Controls;

public partial class EnglishKeyboard : ContentView
{
	public EnglishKeyboard(KeyboardViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}