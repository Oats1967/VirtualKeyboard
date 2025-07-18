using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Messages;
using VirtualKeyboard.ViewModels;
using ILogger = Microsoft.Extensions.Logging.ILogger;


namespace VirtualKeyboard.Services
{
    public abstract class WindowService : IWindowService
    {

        private readonly ILogger _logger;

        private readonly ILayoutService _layoutSettings;

        private static int DisplayWidth => (int)DeviceDisplay.MainDisplayInfo.Width;
        private static int DisplayHeight => (int)DeviceDisplay.MainDisplayInfo.Height;


        public WindowService(ILogger<WindowService> logger, ILayoutService layoutSettings)
        {
            _logger = logger;
            _layoutSettings = layoutSettings;
            
            WeakReferenceMessenger.Default.RegisterAll(this);
        }
        public abstract void ResizeWindow(int x, int y, int width, int height, int cornerRadius = 16);   // needs to access platform specific APIs
        public void Receive(TKSetShow message)

        {
            _logger.LogInformation($"TKSetShow received");
            if (!_layoutSettings.ContainsKey(message.Layout))
            {
                _logger.LogWarning("TKSetShow: Layout {LAYOUT} not supported", message.Layout);
                return;
            }

            var x = _layoutSettings[message.Layout].X;
            var y = _layoutSettings[message.Layout].Y;
            var width = _layoutSettings[message.Layout].Width;
            var height = _layoutSettings[message.Layout].Height;

            WeakReferenceMessenger.Default.Send(new LayoutChangedMessage(message.Layout));

            ResizeWindow((int)x, (int)y, (int)width, (int)height);

            _logger.LogInformation("Window was opened: x: {X} , y: {Y} , w: {Width}, h: {Height}",x,y,width,height);

            
        }

        public void Receive(TKSetHide message)
        {
            _logger.LogInformation($"TKSetHide received");
            ResizeWindow(0, 0, 0, 0);
            _logger.LogInformation($"Window was closed");
        }

        public void Receive(TKSetShowPoint message)
        {
            _logger.LogInformation($"TKSetShowPoint received");

            if (!_layoutSettings.ContainsKey(message.Layout))
            {
                _logger.LogWarning("TKSetShowPoint: Layout {LAYOUT} not supported", message.Layout);
                return;
            }
           

            var width = _layoutSettings[message.Layout].Width;
            var height = _layoutSettings[message.Layout].Height;
            var x = message.X;
            var y = message.Y;
           

            if ((x + width > DisplayWidth) || (y + height > DisplayHeight))
            {
                _logger.LogWarning(
                    "Window position out of bounds: x={X}, y={Y}, width={Width}, height={Height} exceeds screen (DisplayWidth={DisplayWidth}, DisplayHeight={DisplayHeight})",
                    x, y, width, height, DisplayWidth, DisplayHeight);
                return;
            }
            WeakReferenceMessenger.Default.Send(new LayoutChangedMessage(message.Layout));



            _layoutSettings[message.Layout].X = x;
            _layoutSettings[message.Layout].Y = y;

            _logger.LogInformation("{LAYOUT} coordinates were set to x: {X} , y: {Y} ", message.Layout,x,y);

            ResizeWindow(x, y, (int)width, (int)height);
            _logger.LogInformation($"Window was opened: x: {x} , y: {y} , w: {width}, h: {height}");

          

        }

        public void Receive(TkSetSize message)
        {
            _logger.LogInformation($"TKSetSize received");

            if (!_layoutSettings.ContainsKey(message.Layout))
            {
                _logger.LogWarning("TKSetSize: Layout {LAYOUT} not supported", message.Layout);
                return;
            }


            var x = _layoutSettings[message.Layout].X;
            var y = _layoutSettings[message.Layout].Y;
            var width = _layoutSettings[message.Layout].FullWidth * ((double)message.Percentage/100);
            var height = _layoutSettings[message.Layout].FullHeight * ((double)message.Percentage / 100);

            if ((x + width > DisplayWidth) || (y + height > DisplayHeight))
            {
                _logger.LogWarning(
                    "Resized window would exceed screen bounds: x={X}, y={Y}, width={Width}, height={Height} (DisplayWidth={DisplayWidth}, DisplayHeight={DisplayHeight})",
                    x, y, width, height, DisplayWidth, DisplayHeight);
                return;
            }


            _layoutSettings[message.Layout].Width = width;
            _layoutSettings[message.Layout].Height = height;

            _logger.LogInformation("Keyboard size was set to width: {Width} , height: {Height}",width,height);

        }

    }
}
