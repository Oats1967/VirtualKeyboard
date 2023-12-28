using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Pages;

[QueryProperty("Size", "Size")]
public partial class NumericKeyboardPage : ContentPage
{
   public string Size { get; set; }
    public NumericKeyboardPage(NumericKeyboardViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
       
        base.OnNavigatedTo(args);
        var s = Size.Split(',').Select(int.Parse).ToArray();
        WindowSizeService.ResizeWindow(s[0], s[1], s[2], s[3]);
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
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