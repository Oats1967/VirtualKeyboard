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

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        WindowSizeService.ResizeWindow(0, 0, 0, 0);
    }

    

    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}