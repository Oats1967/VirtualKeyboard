using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VirtualKeyboard.Services
{

    public class LayoutData
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public double FullWidth { get; set; }
        public double FullHeight { get; set; }
    }
    public class LayoutService : ILayoutService
    {
        private readonly Lazy<Dictionary<Layouts, LayoutData>> _layouts =
            new(() => InitializeLayouts());

        private static Dictionary<Layouts, LayoutData> InitializeLayouts()
        {
            var ratio = (alpha: 0.35, numeric: 1.2);
            var screenWidth = DeviceDisplay.MainDisplayInfo.Width;
            var screenHeight = DeviceDisplay.MainDisplayInfo.Height;

            var fullNumericHeight = screenHeight;
            var fullNumericWidth = fullNumericHeight / ratio.numeric;

            var fullAlphaWidth = screenWidth;
            var fullAlphaHeight = fullAlphaWidth * ratio.alpha;

            const double defaultX = 0;
            const double defaultY = 0;

            return new()
            {
                [Layouts.Numeric] = new LayoutData
                {
                    X = defaultX,
                    Y = defaultY,
                    Width = fullNumericWidth,
                    Height = fullNumericHeight,
                    FullWidth = fullNumericWidth,
                    FullHeight = fullNumericHeight
                },
                [Layouts.German] = new LayoutData
                {
                    X = defaultX,
                    Y = defaultY,
                    Width = fullAlphaWidth,
                    Height = fullAlphaHeight,
                    FullWidth = fullAlphaWidth,
                    FullHeight = fullAlphaHeight
                },
                [Layouts.English] = new LayoutData
                {
                    X = defaultX,
                    Y = defaultY,
                    Width = fullAlphaWidth,
                    Height = fullAlphaHeight,
                    FullWidth = fullAlphaWidth,
                    FullHeight = fullAlphaHeight
                },
                [Layouts.Dutch] = new LayoutData
                {
                    X = defaultX,
                    Y = defaultY,
                    Width = fullAlphaWidth,
                    Height = fullAlphaHeight,
                    FullWidth = fullAlphaWidth,
                    FullHeight = fullAlphaHeight
                },
                [Layouts.French] = new LayoutData
                {
                    X = defaultX,
                    Y = defaultY,
                    Width = fullAlphaWidth,
                    Height = fullAlphaHeight,
                    FullWidth = fullAlphaWidth,
                    FullHeight = fullAlphaHeight
                },
                [Layouts.Polish] = new LayoutData
                {
                    X = defaultX,
                    Y = defaultY,
                    Width = fullAlphaWidth,
                    Height = fullAlphaHeight,
                    FullWidth = fullAlphaWidth,
                    FullHeight = fullAlphaHeight
                },
            };
        }

        public LayoutData this[Layouts layout]
        {
            get
            {
                if (!_layouts.Value.TryGetValue(layout, out var data))
                    throw new KeyNotFoundException($"Layout '{layout}' not found.");
                return data;
            }
        }

        public bool ContainsKey(Layouts layout) => _layouts.Value.ContainsKey(layout);
    }





}

