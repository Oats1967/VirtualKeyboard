using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;

using WindowsInput;


namespace VirtualKeyboard.Pages;

[QueryProperty("Size", "Size")]
public partial class AlphabeticKeyboardPage : ContentPage
{
    public string Size { get;  set; }
    public AlphabeticKeyboardPage(AlphabeticKeyboardViewModel viewModel)
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
        var button = sender as Button;
        var c = button?.Text;
#if WINDOWS
        // SendKeys dll
#endif
    }

    private void Enter_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

}