
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Controls;

public partial class GermanKeyboard : ContentView
{
	public GermanKeyboard(MainPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}