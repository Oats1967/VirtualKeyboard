

using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Controls;

public partial class EnglishKeyboard : ContentView
{
	public EnglishKeyboard(MainPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}