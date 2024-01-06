using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Pages;

namespace VirtualKeyboard.Commands
{
    public class TKSetSize
    {
        public TKSetSize(Layouts layout, byte percentage)
        {
            Layout = layout;
            percentage = (byte) Math.Max(0, Math.Min((int)percentage, 100));

            double screenHeight = DeviceDisplay.MainDisplayInfo.Height;
            double screenWidth = DeviceDisplay.MainDisplayInfo.Width;
            switch (layout)
            {
                case Layouts.German:
                    Width = (int)(screenWidth * ((double)percentage / 100)); ;
                    Height = (int)(Width * GermanKeyboardPage.Ratio);
                    break;
                case Layouts.Numeric:
                    Height = (int)(screenHeight * ((double)percentage / 100)); ;
                    Width = (int)(Height / NumericKeyboardPage.Ratio);
                    break;
                default:
                    return;
            }
        }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Layouts Layout { get; private set; }

    }
}
