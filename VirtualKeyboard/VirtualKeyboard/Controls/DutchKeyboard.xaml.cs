

using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Controls;

public partial class DutchKeyboard : ContentView
{
	public DutchKeyboard(MainPageViewModel viewModel)
	{
		BindingContext = viewModel;
		
		InitializeComponent();
       //Loaded += DutchKeyboard_Loaded;
	}

    private void DutchKeyboard_Loaded(object? sender, EventArgs e)
    {
       
    }
}