

using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Controls;

public partial class EnglishKeyboard : Grid
{
	public EnglishKeyboard(MainPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}