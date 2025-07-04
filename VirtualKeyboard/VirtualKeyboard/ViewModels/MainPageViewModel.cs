using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Services;




namespace VirtualKeyboard.ViewModels
{
    public partial class MainPageViewModel : ObservableObject, IRecipient<TKSetShow>, IRecipient<TKSetHide>, IRecipient<TKSetShowPoint>, IRecipient<TKSetSize>
    {
        protected ILogger _logger;

        protected ILayoutService _layoutService;

        [ObservableProperty]
        protected Layouts layout;

        [ObservableProperty]
        private double fontSize;

        [ObservableProperty]
        private double keySpacing;

      

        public MainPageViewModel(ILogger<MainPageViewModel> logger, ILayoutService layoutService)
        {
            _logger = logger;
            _layoutService = layoutService;

            // Default initialization
            Layout = Layouts.Numeric;
            var window = Application.Current!.Windows[0];
            window.SizeChanged += Window_SizeChanged;

            WindowSizeService.ResizeWindow(0,0,0,0);
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        

        public void Receive(TKSetShow message)
        {
            _logger.LogInformation($"TKSetShow received");
            if (!_layoutService.Dictionary.ContainsKey(message.Layout))
            {
                _logger.LogInformation($"TKSetShow: Layout {message.Layout} not supported");
                return;
            }

            Layout = message.Layout;

            var x = _layoutService.Dictionary[message.Layout].x;
            var y = _layoutService.Dictionary[message.Layout].y;
            var width = _layoutService.Dictionary[message.Layout].width;
            var height = _layoutService.Dictionary[message.Layout].height;

            x = CalculateCoordinate(x, width, (int)DeviceDisplay.MainDisplayInfo.Width);
            y = CalculateCoordinate(y, height, (int)DeviceDisplay.MainDisplayInfo.Height);
            WindowSizeService.ResizeWindow(x, y, width, height);

            _logger.LogInformation($"Window was opened: x: {x} , y: {y} , w: {width}, h: {height}");
        }

        public void Receive(TKSetHide message)
        {
            _logger.LogInformation($"TKSetHide received");
            WindowSizeService.ResizeWindow(0,0,0,0);
            _logger.LogInformation($"Window was closed: x: {0} , y: {0} , w: {0}, h: {0}");
        }

        public void Receive(TKSetShowPoint message)
        {
            _logger.LogInformation($"TKSetShowPoint received");

            if(!_layoutService.Dictionary.ContainsKey(message.Layout))
            {
                _logger.LogInformation($"TKSetShowPoint: Layout {message.Layout} not supported");
                return;
            }
            Layout = message.Layout;

            var width = _layoutService.Dictionary[message.Layout].width;
            var height = _layoutService.Dictionary[message.Layout].height;
            var x = message.X;
            var y = message.Y;
           

            _layoutService.Dictionary[message.Layout] = (x,y,width,height);
            _logger.LogInformation($"{message.Layout} coordinates were set to x: {x} , y: {y} ");

            x = CalculateCoordinate(x, width, (int)DeviceDisplay.MainDisplayInfo.Width);
            y = CalculateCoordinate(y, height, (int)DeviceDisplay.MainDisplayInfo.Height);
            WindowSizeService.ResizeWindow(x, y, width, height);
            _logger.LogInformation($"Window was opened: x: {x} , y: {y} , w: {width}, h: {height}");


        }

        public void Receive(TKSetSize message)
        {
            _logger.LogInformation($"TKSetSize received");

            if (!_layoutService.Dictionary.ContainsKey(message.Layout))
            {
                _logger.LogInformation($"TKSetSize: Layout {message.Layout} not supported");
                return;
            }

            var x = _layoutService.Dictionary[message.Layout].x;
            var y = _layoutService.Dictionary[message.Layout].y;
            var width = message.Width;
            var height = message.Height;

            _layoutService.Dictionary[message.Layout] = (x, y, width, height);
            _logger.LogInformation($"Keyboard size was set to width: {width} , height: {height}");

        }


        #region X,Y Transform 

        // Gets the real X and Y depending on the Displaysize
        private int CalculateCoordinate(int coordinate, int size, int screenSize)
        {
            var endCoordinate = coordinate + size;

            if (endCoordinate > screenSize)
            {
                return Math.Max(0, coordinate - (endCoordinate - screenSize));
            }
            return coordinate;
        }
        #endregion





        #region Properties changed when wndow is resized

        private void Window_SizeChanged(object? sender, EventArgs e)
        {
            var window = (Window)sender;
            FontSize = GetMappedFontSize(window);
            KeySpacing = GetMappedKeySpacing(window);
        }

        // Adjusts FontSize and KeySpacing Properties depending on Layout and Displaysize
        private double GetMappedFontSize(Window window)
        {
            var resolution = (DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height);
            var fontSize = ResolutionConfig.ResolutionToFontSize[resolution];

            var min = Layout == Layouts.Numeric ? fontSize.numericMin : fontSize.alphaMin;
            var max = Layout == Layouts.Numeric ? fontSize.numericMax : fontSize.alphaMax;

            var currentWidth = window!.Width;
            var maxWidth = DeviceDisplay.Current.MainDisplayInfo.Width;

            return ValueScaler.MapLinear(currentWidth, 0, maxWidth, min, max);
        }

        private double GetMappedKeySpacing(Window window)
        {
            var resolution = (DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height);
            var spacing = ResolutionConfig.ResolutionToSpacing[resolution];

            var min = Layout == Layouts.Numeric ? spacing.numericMin : spacing.alphaMin;
            var max = Layout == Layouts.Numeric ? spacing.numericMax : spacing.alphaMax;

            var currentWidth = window.Width;
            var maxWidth = DeviceDisplay.Current.MainDisplayInfo.Width;

            return ValueScaler.MapLinear(currentWidth, 0, maxWidth, min, max);
        }
        #endregion


    }
}
