using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services.Commands
{
    public class TKSetShowPoint
    {
        public TKSetShowPoint(Layout layout ,int x , int y)
        {
            Layout = layout;
            X = x;
            Y = y;
        }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Layout Layout { get; private set; }
    }
}
