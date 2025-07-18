using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Messages
{
    public class LayoutChangedMessage
    {
        public Layouts Layout { get;  }
        public LayoutChangedMessage(Layouts layout)
        {
            Layout = layout;
        }
    }
}
