using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Pages;

namespace VirtualKeyboard.Services.Commands
{
    public class TKSetSize
    {
        public TKSetSize(Layouts layout ,byte percentage)
        {
            Layout = layout;
            double screenHeight = DeviceDisplay.MainDisplayInfo.Height;
            Height = (int) (screenHeight * ((double)percentage / 100)); ;
            switch (layout)
            {
                case Layouts.German:
                    Width = (int)(Height / GermanKeyboardPage.Ratio);
                    break;
                case Layouts.Numeric:
                    Width = (int)(Height / NumericKeyboardPage.Ratio);
                    break;
                default:
                    return;
            }
        }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Layouts Layout {  get; private set; }

    }
}
