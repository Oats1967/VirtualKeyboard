using System.Runtime.InteropServices;
using System.Text;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;




namespace VirtualKeyboard.Pages;

[QueryProperty("WindowInfo", "WindowInfo")]
public partial class GermanKeyboardPage : ContentPage
{
    public  static readonly double Ratio = 0.4;
    public string WindowInfo { get; set; } = string.Empty;
    public GermanKeyboardPage(AlphabeticKeyboardViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {

        base.OnNavigatedTo(args);
        var s = WindowInfo.Split('|').Select(int.Parse).ToArray();
        WindowSizeService.ResizeWindow(s[0], s[1], s[2], s[3]);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        WindowSizeService.ResizeWindow(0, 0, 0, 0);
    }


    

}