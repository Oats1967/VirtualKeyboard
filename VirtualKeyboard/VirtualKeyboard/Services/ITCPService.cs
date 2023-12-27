using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{
    public interface ITCPService
    {
       // Declare the delegate (if using non-generic pattern).
         public delegate void EventHandler(object sender, EventArgs e);
         public event EventHandler OnKeyboardSelected;
        public void StartAsync();
    }
}
