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
            WeakReferenceMessenger.Default.Send(new LayoutChangedMessage(message.Layout));

            var x = _layoutSettings[message.Layout].X;
            var y = _layoutSettings[message.Layout].Y;
            var width = _layoutSettings[message.Layout].Width;
            var height = _layoutSettings[message.Layout].Height;

           

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
            WeakReferenceMessenger.Default.Send(new LayoutChangedMessage(message.Layout));

            _layoutSettings[message.Layout].X = ShiftInsideBounds(message.X, _layoutSettings[message.Layout].Width, DisplayWidth);
            _layoutSettings[message.Layout].Y = ShiftInsideBounds(message.Y, _layoutSettings[message.Layout].Height, DisplayHeight);

            var x = _layoutSettings[message.Layout].X;
            var y = _layoutSettings[message.Layout].Y;
            var width = _layoutSettings[message.Layout].Width;
            var height = _layoutSettings[message.Layout].Height;

            _logger.LogInformation("{LAYOUT}\nx: {X} , y: {Y}\nwidth: {WIDTH} , height: {HEIGHT} ", message.Layout, x, y,width,height);

            ResizeWindow((int)x, (int)y, (int)width, (int)height);
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

            _layoutSettings[message.Layout].Width = _layoutSettings[message.Layout].FullWidth * ((double)message.Percentage / 100); ;
            _layoutSettings[message.Layout].Height = _layoutSettings[message.Layout].FullHeight * ((double)message.Percentage / 100); ;
            _layoutSettings[message.Layout].X = ShiftInsideBounds(_layoutSettings[message.Layout].Width, _layoutSettings[message.Layout].Width, DisplayWidth);
            _layoutSettings[message.Layout].Y = ShiftInsideBounds(_layoutSettings[message.Layout].Height, _layoutSettings[message.Layout].Height, DisplayHeight);
            

            var x = _layoutSettings[message.Layout].X;
            var y = _layoutSettings[message.Layout].Y;
            var width = _layoutSettings[message.Layout].Width;
            var height = _layoutSettings[message.Layout].Height;

            _logger.LogInformation("{LAYOUT}\nx: {X} , y: {Y}\nwidth: {WIDTH} , height: {HEIGHT} ", message.Layout, x, y, width, height);

        }


        private static double ShiftInsideBounds(double c, double s, double max) => c + s > max ? Math.Max(0, c - (c + s - max)) : c;


    }
}
