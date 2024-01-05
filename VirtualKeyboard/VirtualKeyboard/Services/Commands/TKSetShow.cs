using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services.Commands
{
    public class TKSetShow
    {
        public TKSetShow(Layout layout) 
        {
            Layout = layout;
        }
        public Layout Layout {  get; private set; }
    }
}
