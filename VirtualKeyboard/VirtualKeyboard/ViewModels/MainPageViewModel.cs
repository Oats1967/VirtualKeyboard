using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Services;




namespace VirtualKeyboard.ViewModels
{
    public partial class MainPageViewModel : ObservableObject, IRecipient<TKSetShow>, IRecipient<TKSetHide>, IRecipient<TKSetShowPoint>, IRecipient<TKSetSize>
    {

        protected ILogger _logger;
        protected int DefaultWidth => Layout switch
        {
            Layouts.Numeric => (int)(DeviceDisplay.Current.MainDisplayInfo.Height / 2),
            _ => (int)(DeviceDisplay.Current.MainDisplayInfo.Width / 2)
        };
        protected int DefaultHeight
        {
            get
            {
                var resolution = (DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height);
                return Layout switch
                {
                    Layouts.Numeric => (int)(DefaultWidth * ResolutionConfig.ResolutionToKeyboardRatio[resolution].numericRatio),
                    _ => (int)(DefaultWidth * ResolutionConfig.ResolutionToKeyboardRatio[resolution].alphaRatio)
                };
            }
        }
        protected int DefaultX 
            => (int)((DeviceDisplay.Current.MainDisplayInfo.Width - Width) / 2);
        protected int DefaultY 
            => (int)((DeviceDisplay.Current.MainDisplayInfo.Height - Height) / 2);


        [ObservableProperty]
        protected Layouts layout;

        [ObservableProperty]
        private int x;

        [ObservableProperty]
        private int y;

        [ObservableProperty]
        private int width;

        [ObservableProperty]
        private int height;

        public MainPageViewModel(ILogger<MainPageViewModel> logger)
        {
            _logger = logger;
            Layout = Layouts.Numeric;
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        // Hide WIndow on first Appearance
        public void Appearing(object? sender, EventArgs e)
        {
            var x = 0; var y = 0; var width = 0; var height = 0;    
            WindowSizeService.ResizeWindow(x, y, width, height);
            _logger.LogInformation($"Window was resized to {x} {y} {width} {height}");
        }

        
        public void Receive(TKSetShow message)
        {
            _logger.LogInformation($"TKSetShow received");
            WindowSizeService.ResizeWindow(X, Y, Width, Height);
            _logger.LogInformation($"Window was resized to {x} {y} {width} {height}");
        }

        public void Receive(TKSetHide message)
        {
            _logger.LogInformation($"TKSetHide received");
            var x = 0; var y = 0; var width = 0; var height = 0;
            WindowSizeService.ResizeWindow(x, y, width, height);
            _logger.LogInformation($"Window was resized to {x} {y} {width} {height}");
        }

        public void Receive(TKSetShowPoint message)
        {
            _logger.LogInformation($"TKSetShowPoint received");

            Layout = message.Layout;

            if (Height == 0) Height = DefaultHeight;
            if (Width == 0) Width = DefaultWidth;

            (X,Y,Width,Height) = CorrectOutOfBounds(message.X,message.Y, Width ,Height);
            _logger.LogInformation($"Keyboard was shifted to {X} {Y} {Width} {Height}");
            WindowSizeService.ResizeWindow(X, Y, Width, Height);
        }

        public void Receive(TKSetSize message)
        {
            _logger.LogInformation($"TKSetSize received");

            Layout = message.Layout;

            if (X == 0) X = DefaultX;
            if (Y == 0) Y = DefaultY;

            Width = message.Width;
            Height = message.Height;
            (X,Y,Width,Height) = CorrectOutOfBounds(X,Y, message.Width, message.Height);
            _logger.LogInformation($"Keyboard was resized to {X} {Y} {Width} {Height}");
            WindowSizeService.ResizeWindow(X, Y, Width, Height);
        }
        //

        #region X,Y Transform 
        // Gets the real X and Y depending on the DeviceDisplay
        private (int x, int y, int width, int height) CorrectOutOfBounds(int x, int y, int width, int height)
        {
            x = CorrectDimension(x, width, (int)DeviceDisplay.MainDisplayInfo.Width);
            y = CorrectDimension(y, height, (int)DeviceDisplay.MainDisplayInfo.Height);
            return (x, y, width, height);
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
        #endregion



    }
}
