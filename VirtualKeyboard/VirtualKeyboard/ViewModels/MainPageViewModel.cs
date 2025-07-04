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
        protected (double current, double max) SizeRef =>
           (Application.Current!.Windows[0].Width, DeviceDisplay.Current.MainDisplayInfo.Width);


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

        [ObservableProperty]
        private double fontSize;

        [ObservableProperty]
        private double keySpacing;

      

        public MainPageViewModel(ILogger<MainPageViewModel> logger)
        {
            // Default initialization
            Layout = Layouts.Numeric; 
            X = 0; Y = 0; Width = 0; Height = 0;
            _logger = logger;
            ResizeWindow(X, Y, Width, Height);
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

   
        
        public void Receive(TKSetShow message)
        {
            _logger.LogInformation($"TKSetShow received");
            if (message.Layout.Equals(Layouts.NotUsed))
            {
                _logger.LogInformation($"TKSetShow: Layout NotUsed is not supported");
                return;
            }
            ResizeWindow(X, Y, Width, Height);
            _logger.LogInformation($"Window was resized to {x} {y} {width} {height}");
        }

        public void Receive(TKSetHide message)
        {
            _logger.LogInformation($"TKSetHide received");
            ResizeWindow(0, 0, 0, 0);
            _logger.LogInformation($"Window was resized to {x} {y} {width} {height}");
        }

        public void Receive(TKSetShowPoint message)
        {
            _logger.LogInformation($"TKSetShowPoint received");

            if (message.Layout.Equals(Layouts.NotUsed))
            {
                _logger.LogInformation($"TKSetShow: Layout NotUsed is not supported");
                return;
            }
            // Unexpected Layout-Change
            if (!Layout.Equals(message.Layout))
            {
                Layout = message.Layout;
                Height = DefaultHeight; Width = DefaultWidth;
            }

            // Size was not set
            if (Height == 0) Height = DefaultHeight;
            if (Width == 0) Width = DefaultWidth;

            (X,Y,Width,Height) = CalculateWindow(message.X,message.Y, Width ,Height);
           
            ResizeWindow(X, Y, Width, Height);
            _logger.LogInformation($"Keyboard was shifted to {X} {Y} {Width} {Height}");

        }

        public void Receive(TKSetSize message)
        {
            _logger.LogInformation($"TKSetSize received");

            if (message.Layout.Equals(Layouts.NotUsed))
            {
                _logger.LogInformation($"TKSetShow: Layout NotUsed is not supported");
                return;
            }

            // Unexpected Layout-Change
            if (!Layout.Equals( message.Layout))
            {
                Layout = message.Layout;
                X = DefaultX; Y = DefaultY;
            }
            
            // ShowPoint was not set
            if (X == 0) X = DefaultX;
            if (Y == 0) Y = DefaultY;

            // Set size
            (X,Y,Width,Height) = CalculateWindow(X,Y, message.Width, message.Height);
         
            ResizeWindow(X,Y,Width,Height);
            _logger.LogInformation($"Keyboard was resized to {X} {Y} {Width} {Height}");
        }


        #region X,Y Transform 
        // Returns the window depending on the Displaysize
        private (int x, int y, int width, int height) CalculateWindow(int x, int y, int width, int height)
        {
            x = CalculateCoordinate(x, width, (int)DeviceDisplay.MainDisplayInfo.Width);
            y = CalculateCoordinate(y, height, (int)DeviceDisplay.MainDisplayInfo.Height);
            return (x, y, width, height);
        }

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

        private void ResizeWindow(int x, int y, int width , int height)
        {
            WindowSizeService.ResizeWindow(x, y, width, height);
            (FontSize, KeySpacing) = ApplyKeyAdjustments();
        }
        // Adjusts FontSize and KeySpacing Properties depending on Layout and Displaysize
        private (double fontSize,double keySpacing) ApplyKeyAdjustments()
        {
            _logger.LogInformation("SizeChanged KeyboardViewModel");
            var fontSize = ResolutionConfig.ResolutionToFontSize[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];
            var keySpacing = ResolutionConfig.ResolutionToSpacing[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];
            var fontsizeMin = Layout switch
            {
                Layouts.Numeric => fontSize.numericMin,
                _ => fontSize.alphaMin
            };
            var fontsizeMax = Layout switch
            {
                Layouts.Numeric => fontSize.numericMax,
                _ => fontSize.alphaMax
            };
            var keySpacingMin = Layout switch
            {
                Layouts.Numeric => keySpacing.numericMin,
                _ => keySpacing.alphaMin
            };
            var keySpacingMax = Layout switch
            {
                Layouts.Numeric => keySpacing.numericMax,
                _ => keySpacing.alphaMax
            };
            var mappedFontSize = ValueScaler.MapLinear(SizeRef.current, 0, SizeRef.max, fontsizeMin, fontsizeMax);
            var mappedKeySpacing = ValueScaler.MapLinear(SizeRef.current, 0, SizeRef.max, keySpacingMin, keySpacingMax);
            return (mappedFontSize, mappedKeySpacing);
        }


    }
}
