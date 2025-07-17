
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Controls;

public partial class GermanKeyboard : Grid
{
	public GermanKeyboard(MainPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}