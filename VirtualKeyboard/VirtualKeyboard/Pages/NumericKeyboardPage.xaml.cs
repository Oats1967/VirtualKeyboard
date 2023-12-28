using System.Runtime.InteropServices;
using System.Text;
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


    [ComVisible(true)]
    [DllImport("SendKeysMAUI64.dll",EntryPoint = "SendKeys", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern int SendKeys(byte arg);

    private void Button_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var buttonText = button?.Text;
        if (string.IsNullOrEmpty(buttonText)) return;
 
        var utf8Bytes = Encoding.UTF8.GetBytes(buttonText);
        var firstByte = utf8Bytes[0];
        // Use the firstByte as needed
#if WINDOWS
        SendKeys(firstByte);
#endif
        

    }

    private void Enter_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}