using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{
    public class LayoutService : ILayoutService
    {
        public Dictionary<Layouts, (int x, int y, int width, int height)> Dictionary { get; private set; } = new();

        public LayoutService()
        {
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo;

            int screenWidth = (int)displayInfo.Width;
            int screenHeight = (int)displayInfo.Height;
            var resolution = (screenWidth, screenHeight);

            Dictionary = new Dictionary<Layouts, (int x, int y, int width, int height)>
            {
                [Layouts.Numeric] = GetLayoutData(Layouts.Numeric, screenWidth, screenHeight, resolution),
                [Layouts.German] = GetLayoutData(Layouts.German, screenWidth, screenHeight, resolution),
                [Layouts.English] = GetLayoutData(Layouts.English, screenWidth, screenHeight, resolution),
                [Layouts.Dutch] = GetLayoutData(Layouts.Dutch, screenWidth, screenHeight, resolution),
                [Layouts.French] = GetLayoutData(Layouts.French, screenWidth, screenHeight, resolution),
                [Layouts.Polish] = GetLayoutData(Layouts.Polish, screenWidth, screenHeight, resolution),
            };
        }

        private (int x, int y, int width, int height) GetLayoutData(Layouts layout, int screenWidth, int screenHeight, (int, int) resolution)
        {
            int width = layout == Layouts.Numeric
                ? screenHeight / 2
                : screenWidth / 2;

            double ratio = layout == Layouts.Numeric
                ? ResolutionConfig.ResolutionToKeyboardRatio[resolution].numericRatio
                : ResolutionConfig.ResolutionToKeyboardRatio[resolution].alphaRatio;

            int height = (int)(width * ratio);

            int x = (screenWidth - width) / 2;
            int y = (screenHeight - height) / 2;

            return (x, y, width, height);
        }
    }


}
