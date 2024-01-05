using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services.Commands
{
    public class TKSetSize
    {
        public TKSetSize(Layout layout ,byte percentage)
        {
            Layout = layout;
            Percentage = percentage;
        }
        public byte Percentage { get; private set; }
        public Layout Layout {  get; private set; }

    }
}
