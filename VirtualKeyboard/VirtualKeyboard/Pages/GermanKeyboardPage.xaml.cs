using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;




namespace VirtualKeyboard.Pages;


public partial class GermanKeyboardPage : ContentPage , IRecipient<TKSetSize>, IRecipient<TKSetShowPoint>
{
    public  static readonly double Ratio = 0.35;
   
    private int WindowX { get; set; } = 0;
    private int WindowY { get; set; } = 0;

    private int WindowWidth { get; set; } 
    private int WindowHeight { get; set; } 

    private AlphabeticKeyboardViewModel ViewModel => (AlphabeticKeyboardViewModel)BindingContext;
    public GermanKeyboardPage(AlphabeticKeyboardViewModel viewModel)
	{
        WindowWidth = (int) (DeviceDisplay.Current.MainDisplayInfo.Width/2);
        WindowHeight = (int) (WindowWidth * Ratio);
        WindowX = (int)((DeviceDisplay.Current.MainDisplayInfo.Width - WindowWidth) / 2);
        WindowY = (int)((DeviceDisplay.Current.MainDisplayInfo.Height - WindowHeight) / 2);
        WeakReferenceMessenger.Default.Register<TKSetSize>(this);
        WeakReferenceMessenger.Default.Register<TKSetShowPoint>(this);
        InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        var maxWidth = DeviceDisplay.Current.MainDisplayInfo.Width;
        var currentWidth = Application.Current!.Windows[0].Width;
      
        var fontSize = ResolutionConfig.ResolutionToFontSize[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];
        var keySpacing = ResolutionConfig.ResolutionToSpacing[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];

        ViewModel.FontSize = ValueScaler.MapLinear(currentWidth, 0, maxWidth, fontSize.alphaMin, fontSize.alphaMax);
        ViewModel.KeySpacing = ValueScaler.MapLinear(currentWidth, 0,  maxWidth, keySpacing.alphaMin, keySpacing.alphaMax);
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
        if (message.Layout != Layouts.German) return;
        WindowWidth = message.Width;
        WindowHeight = message.Height;
        CorrectOutOfBounds();
        if (Shell.Current!.CurrentPage is GermanKeyboardPage)
        {
            WindowSizeService.ResizeWindow(WindowX, WindowY, WindowWidth, WindowHeight);
        }
    }

    void IRecipient<TKSetShowPoint>.Receive(TKSetShowPoint message)
    {
        if (message.Layout != Layouts.German) return;
        WindowX = message.X;
        WindowY = message.Y;
        CorrectOutOfBounds();
        if(Shell.Current!.CurrentPage is GermanKeyboardPage)
        {
           WindowSizeService.ResizeWindow(WindowX, WindowY, WindowWidth, WindowHeight);
        }
    }

    private void CorrectOutOfBounds()
    {
        WindowX = CorrectDimension( WindowX, WindowWidth, (int)DeviceDisplay.MainDisplayInfo.Width);
        WindowY = CorrectDimension( WindowY, WindowHeight, (int)DeviceDisplay.MainDisplayInfo.Height);
    }

    private int CorrectDimension( int coordinate, int size, int screenSize)
    {
        var endCoordinate = coordinate + size;

        if (endCoordinate > screenSize)
        {
            return Math.Max(0, coordinate - (endCoordinate - screenSize));
        }
        return coordinate;
    }

}