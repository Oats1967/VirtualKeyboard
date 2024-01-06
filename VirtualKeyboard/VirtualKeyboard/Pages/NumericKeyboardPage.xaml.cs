using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Pages;


public partial class NumericKeyboardPage : ContentPage, IRecipient<TKSetSize>, IRecipient<TKSetShowPoint>
{
    public static readonly double Ratio = 1.2;
    private int WindowX { get; set; } = 0;
    private int WindowY { get; set; } = 0;

    private int WindowWidth { get; set; } = 400;
    private int WindowHeight { get; set; } = 480;
    public NumericKeyboardPage(NumericKeyboardViewModel viewModel)
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
        if (message.Layout != Layouts.Numeric) return;
        WindowWidth = message.Width;
        WindowHeight = message.Height;
        CorrectOutOfBounds();
        if (Shell.Current.CurrentPage is NumericKeyboardPage)
        {
            WindowSizeService.ResizeWindow(WindowX, WindowY, WindowWidth, WindowHeight);
        }
    }

    void IRecipient<TKSetShowPoint>.Receive(TKSetShowPoint message)
    {
        if (message.Layout != Layouts.Numeric) return;
        WindowX = message.X;
        WindowY = message.Y;
        CorrectOutOfBounds();
        if (Shell.Current!.CurrentPage is NumericKeyboardPage)
        {
            WindowSizeService.ResizeWindow(WindowX, WindowY, WindowWidth, WindowHeight);
        }
    }

    private void CorrectOutOfBounds()
    {
        WindowX = CorrectDimension(WindowX, WindowWidth, (int)DeviceDisplay.MainDisplayInfo.Width);
        WindowY = CorrectDimension(WindowY, WindowHeight, (int)DeviceDisplay.MainDisplayInfo.Height);
    }

    private int CorrectDimension(int coordinate, int size, int screenSize)
    {
        var endCoordinate = coordinate + size;

        if (endCoordinate > screenSize)
        {
            return Math.Max(0, coordinate - (endCoordinate - screenSize));
        }
        return coordinate;
    }


}