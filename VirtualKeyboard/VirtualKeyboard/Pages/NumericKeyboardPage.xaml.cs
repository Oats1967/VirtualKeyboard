using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;
namespace VirtualKeyboard.Pages;


public partial class NumericKeyboardPage : ContentPage, IRecipient<TKSetSize>, IRecipient<TKSetShowPoint>
{
    public static readonly double Ratio = 1.2;
    private const double MaxSpacing = 15;
    private const double MinSpacing = 0;
    private const double MaxFontSize = 60;
    private const double MinFontSize = 5;
    private int WindowX { get; set; } = 0;
    private int WindowY { get; set; } = 0;

    private int WindowWidth { get; set; } 
    private int WindowHeight { get; set; } 

    private NumericKeyboardViewModel ViewModel => (NumericKeyboardViewModel)BindingContext;
    public NumericKeyboardPage(NumericKeyboardViewModel viewModel)
	{
        WindowWidth = (int)(DeviceDisplay.Current.MainDisplayInfo.Height/2);
        WindowHeight = (int) (WindowWidth * Ratio);
        WindowX = (int)((DeviceDisplay.Current.MainDisplayInfo.Width - WindowWidth)/2) ;
        WindowY = (int)((DeviceDisplay.Current.MainDisplayInfo.Height - WindowHeight) / 2);

        WeakReferenceMessenger.Default.Register<TKSetSize>(this);
        WeakReferenceMessenger.Default.Register<TKSetShowPoint>(this);
        InitializeComponent();
		BindingContext = viewModel;
	}



    protected override void OnSizeAllocated(double width, double height)
    {

        var maxHeight = DeviceDisplay.Current.MainDisplayInfo.Height; 
        var currentHeight = Application.Current!.Windows[0].Height;

        var fontSize = ResolutionConfig.ResolutionToFontSize[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];
        var keySpacing = ResolutionConfig.ResolutionToSpacing[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];

        ViewModel.FontSize = ValueScaler.MapLinear(currentHeight, 0, maxHeight ,fontSize.numericMin, fontSize.numericMax);
        ViewModel.KeySpacing = ValueScaler.MapLinear(currentHeight, 0, maxHeight, keySpacing.numericMin, keySpacing.numericMax);
        base.OnSizeAllocated(width, height);

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