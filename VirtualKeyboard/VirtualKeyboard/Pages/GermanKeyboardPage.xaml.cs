using System.Runtime.InteropServices;
using System.Text;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;




namespace VirtualKeyboard.Pages;

[QueryProperty("Size", "Size")]
public partial class GermanKeyboardPage : ContentPage
{
    public string Size { get; set; } = string.Empty;
    public GermanKeyboardPage(AlphabeticKeyboardViewModel viewModel)
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


    

}