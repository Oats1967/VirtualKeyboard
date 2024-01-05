using CommunityToolkit.Mvvm.Messaging;
using System.Runtime.InteropServices;
using System.Text;
using VirtualKeyboard.Services;
using VirtualKeyboard.Services.Commands;
using VirtualKeyboard.ViewModels;




namespace VirtualKeyboard.Pages;


public partial class GermanKeyboardPage : ContentPage , IRecipient<TKSetSize>, IRecipient<TKSetShowPoint>
{
    public  static readonly double Ratio = 0.4;
    private int WindowX { get; set; } = 0;
    private int WindowY { get; set; } = 0;

    private int WindowWidth { get; set; } = 1200;
    private int WindowHeight { get; set; } = 480;
    public GermanKeyboardPage(AlphabeticKeyboardViewModel viewModel)
	{
        WeakReferenceMessenger.Default.Register<TKSetSize>(this);
        WeakReferenceMessenger.Default.Register<TKSetShowPoint>(this);
        InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {

        base.OnNavigatedTo(args);
        WindowSizeService.ResizeWindow(WindowX, WindowY, WindowWidth, WindowHeight);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        WindowSizeService.ResizeWindow(0, 0, 0, 0);
    }

    void IRecipient<TKSetSize>.Receive(TKSetSize message)
    {
        if (message.Layout != Services.Commands.Layout.German) return;
        WindowWidth = message.Width;
        WindowHeight = message.Height;
        if (Application.Current!.MainPage is GermanKeyboardPage)
        {
            WindowSizeService.ResizeWindow(WindowX, WindowY, WindowWidth, WindowHeight);
        }
    }

    void IRecipient<TKSetShowPoint>.Receive(TKSetShowPoint message)
    {
        if (message.Layout != Services.Commands.Layout.German) return;
        WindowX = message.X;
        WindowY = message.Y;
        if(Application.Current!.MainPage is GermanKeyboardPage)
        {
           WindowSizeService.ResizeWindow(WindowX, WindowY, WindowWidth, WindowHeight);
        }
    }

}