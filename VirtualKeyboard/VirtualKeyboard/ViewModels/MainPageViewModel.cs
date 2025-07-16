using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Controls;
using VirtualKeyboard.Services;




namespace VirtualKeyboard.ViewModels
{
    public partial class MainPageViewModel : ObservableObject, IRecipient<TKSetShow>, IRecipient<TKSetHide>, IRecipient<TKSetShowPoint>, IRecipient<TKSetSize>
    {
        protected ILogger _logger;

        protected ILayoutService _layoutService;

        protected IKeyboardService _keyboardService;

        protected IWindowManager _windowManager;

        [ObservableProperty]
        protected Layouts layout;

        [ObservableProperty]
        private double fontSize;

        [ObservableProperty]
        private double keySpacing;

      

        public MainPageViewModel(ILogger<MainPageViewModel> logger, IKeyboardService keyboardService, ILayoutService layoutService, IWindowManager windowManager)
        {
            _logger = logger;
            _layoutService = layoutService;
            _keyboardService = keyboardService;
            _windowManager = windowManager;

            // Default initialization
            Layout = Layouts.Numeric;
            
            WeakReferenceMessenger.Default.RegisterAll(this);
        }


        #region Keyboard - Control Behaviour

        [ObservableProperty]
        private bool locked;

        [ObservableProperty]
        private bool capsLock;

        [RelayCommand]
        public void LeftPressed()
        {
            _keyboardService.SendKey(0x25);
        }

        [RelayCommand]
        public void RightPressed()
        {
            _keyboardService.SendKey(0x27);
        }

        [RelayCommand]
        public void EnterPressed()
        {
            _keyboardService.SendKey(0x0D);

        }


        [RelayCommand]
        public void Lock()
        {

            CapsLock = !CapsLock;
            Locked = CapsLock;
        }

        [RelayCommand]
        public void CapsLockPressed()
        {
            Locked = false;
            CapsLock = !CapsLock;
        }

        [RelayCommand]
        public void KeyPressed(string key)
        {
            if (!Locked)
            {
                CapsLock = false;
            }
            _keyboardService.SendKey(Convert.ToChar(key));
        }

        [RelayCommand]
        public void BackspacePressed()
        {
            if (!Locked)
            {
                CapsLock = false;
            }
            _keyboardService.SendKey(0x08);
        }

        #endregion

        // Windowsize



        public void Receive(TKSetShow message)
        
        {
            _logger.LogInformation($"TKSetShow received");
            if (!_layoutService.Layouts.ContainsKey(message.Layout))
            {
                _logger.LogInformation($"TKSetShow: Layout {message.Layout} not supported");
                return;
            }

            Layout = message.Layout;

            var x = _layoutService.Layouts[message.Layout].x;
            var y = _layoutService.Layouts[message.Layout].y;
            var width = _layoutService.Layouts[message.Layout].width;
            var height = _layoutService.Layouts[message.Layout].height;

            x = CalculateCoordinate(x, width, (int)DeviceDisplay.MainDisplayInfo.Width);
            y = CalculateCoordinate(y, height, (int)DeviceDisplay.MainDisplayInfo.Height);
            _windowManager.ResizeWindow(x, y, width, height);

            _logger.LogInformation($"Window was opened: x: {x} , y: {y} , w: {width}, h: {height}");
        }

        public void Receive(TKSetHide message)
        {
            _logger.LogInformation($"TKSetHide received");
            _windowManager.ResizeWindow(0,0,0,0,16);
            _logger.LogInformation($"Window was closed: x: {0} , y: {0} , w: {0}, h: {0}");
        }

        public void Receive(TKSetShowPoint message)
        {
            _logger.LogInformation($"TKSetShowPoint received");

            if(!_layoutService.Layouts.ContainsKey(message.Layout))
            {
                _logger.LogInformation($"TKSetShowPoint: Layout {message.Layout} not supported");
                return;
            }
            Layout = message.Layout;

            var width = _layoutService.Layouts[message.Layout].width;
            var height = _layoutService.Layouts[message.Layout].height;
            var x = message.X;
            var y = message.Y;
           

            _layoutService.Layouts[message.Layout] = (x,y,width,height);
            _logger.LogInformation($"{message.Layout} coordinates were set to x: {x} , y: {y} ");


            x = CalculateCoordinate(x, width, (int)DeviceDisplay.MainDisplayInfo.Width);
            y = CalculateCoordinate(y, height, (int)DeviceDisplay.MainDisplayInfo.Height);
            _windowManager.ResizeWindow(x, y, width, height);
            _logger.LogInformation($"Window was opened: x: {x} , y: {y} , w: {width}, h: {height}");


        }

        public void Receive(TKSetSize message)
        {
            _logger.LogInformation($"TKSetSize received");

            if (!_layoutService.Layouts.ContainsKey(message.Layout))
            {
                _logger.LogInformation($"TKSetSize: Layout {message.Layout} not supported");
                return;
            }

            var size = CalculateSize(message.Layout, message.Percentage);

            var x = _layoutService.Layouts[message.Layout].x;
            var y = _layoutService.Layouts[message.Layout].y;
            var width = size.width;
            var height = size.height;

            _layoutService.Layouts[message.Layout] = (x, y, width, height);
            _logger.LogInformation($"Keyboard size was set to width: {width} , height: {height}");

        }


        #region Transform funcs

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


        public (int width, int height) CalculateSize(Layouts layout, int percentage)
        {
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
            var screenWidth = displayInfo.Width;
            var screenHeight = displayInfo.Height;

            var alphaRatio = (double)Application.Current!.Resources["AlphaRatio"];
            var numericRatio = (double)Application.Current!.Resources["NumericRatio"];

            double ratio = layout == Layouts.Numeric ? numericRatio : alphaRatio;
           

            if (layout == Layouts.Numeric)
            {
                var height = (int)(screenHeight * percentage/100);
                var width = (int)(height / ratio);
                return (width, height);
            }
            else
            {
                var width = (int)(screenWidth * percentage/100);
                var height = (int)(width * ratio);
                return (width, height);
            }
        }
        #endregion





        #region Properties changed when window is resized

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
