using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Pages;

[QueryProperty("WindowInfo", "WindowInfo")]
public partial class NumericKeyboardPage : ContentPage
{
    public static readonly double Ratio = 1.2;
    public string WindowInfo { get; set; } = string.Empty;
    public NumericKeyboardPage(NumericKeyboardViewModel viewModel)
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