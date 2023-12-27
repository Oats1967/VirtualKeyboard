using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Pages;

public partial class NumericKeyboardPage : ContentPage
{
	public NumericKeyboardPage(NumericKeyboardViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
		
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        WindowSizeService.ResizeWindow(0, 0, 0, 0);
        base.OnDisappearing();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}