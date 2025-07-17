using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.ViewModels;
using ILogger = Microsoft.Extensions.Logging.ILogger;


namespace VirtualKeyboard.Services
{
    public abstract class WindowService : IWindowService
    {

        protected ILogger _logger;

        protected ILayoutSettings _layoutSettings;

       
        public WindowService(ILogger<WindowService> logger, ILayoutSettings layoutSettings)
        {
            _logger = logger;
            _layoutSettings = layoutSettings;
            WeakReferenceMessenger.Default.RegisterAll(this);

        }
        public abstract void ResizeWindow(int x, int y, int width, int height, int cornerRadius = 0);   // needs to access platform specific APIs
        public void Receive(TKSetShow message)

        {
            _logger.LogInformation($"TKSetShow received");
            if (!_layoutSettings.ContainsKey(message.Layout))
            {
                _logger.LogInformation($"TKSetShow: Layout {message.Layout} not supported");
                return;
            }

            WeakReferenceMessenger.Default.Send(new LayoutChangedMessage(layout));

            var x = _layoutSettings[message.Layout].x;
            var y = _layoutSettings[message.Layout].y;
            var width = _layoutSettings[message.Layout].width;
            var height = _layoutSettings[message.Layout].height;

            x = ClampCoordinateToScreen(x, width, (int)DeviceDisplay.MainDisplayInfo.Width);
            y = ClampCoordinateToScreen(y, height, (int)DeviceDisplay.MainDisplayInfo.Height);
            ResizeWindow(x, y, width, height);

            _logger.LogInformation($"Window was opened: x: {x} , y: {y} , w: {width}, h: {height}");
        }

        public void Receive(TKSetHide message)
        {
            _logger.LogInformation($"TKSetHide received");
            ResizeWindow(0, 0, 0, 0);
            _logger.LogInformation($"Window was closed: x: {0} , y: {0} , w: {0}, h: {0}");
        }

        public void Receive(TKSetShowPoint message)
        {
            _logger.LogInformation($"TKSetShowPoint received");

            if (!_layoutSettings.ContainsKey(message.Layout))
            {
                _logger.LogInformation($"TKSetShowPoint: Layout {message.Layout} not supported");
                return;
            }
            WeakReferenceMessenger.Default.Send(new LayoutChangedMessage(layout));

            var width = _layoutSettings[message.Layout].width;
            var height = _layoutSettings[message.Layout].height;
            var x = message.X;
            var y = message.Y;


            _layoutSettings[message.Layout] = (x, y, width, height);
            _logger.LogInformation($"{message.Layout} coordinates were set to x: {x} , y: {y} ");


            x = ClampCoordinateToScreen(x, width, (int)DeviceDisplay.MainDisplayInfo.Width);
            y = ClampCoordinateToScreen(y, height, (int)DeviceDisplay.MainDisplayInfo.Height);
            ResizeWindow(x, y, width, height);
            _logger.LogInformation($"Window was opened: x: {x} , y: {y} , w: {width}, h: {height}");


        }

        public void Receive(TKSetSize message)
        {
            _logger.LogInformation($"TKSetSize received");

            if (!_layoutSettings.ContainsKey(message.Layout))
            {
                _logger.LogInformation($"TKSetSize: Layout {message.Layout} not supported");
                return;
            }

            var size = CalculateSize(message.Layout, message.Percentage);

            var x = _layoutSettings[message.Layout].x;
            var y = _layoutSettings[message.Layout].y;
            var width = size.width;
            var height = size.height;

            _layoutSettings[message.Layout] = (x, y, width, height);
            _logger.LogInformation($"Keyboard size was set to width: {width} , height: {height}");

        }


        #region Transform funcs

        // Gets the real X and Y depending on the Displaysize
        private int ClampCoordinateToScreen(int coordinate, int size, int screenSize) =>
            (coordinate + size) > screenSize ? Math.Max(0, coordinate - ((coordinate + size) - screenSize)) : coordinate;



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
                var height = (int)(screenHeight * percentage / 100);
                var width = (int)(height / ratio);
                return (width, height);
            }
            else
            {
                var width = (int)(screenWidth * percentage / 100);
                var height = (int)(width * ratio);
                return (width, height);
            }
        }
        #endregion

    }
}
