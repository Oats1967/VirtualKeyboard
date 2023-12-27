using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{

    public class ServerEventArgs
    {
        public ServerEventArgs(string text) { Text = text; }
        public string Text { get; } // readonly
    }
    public interface ITCPService
    {
       // Declare the delegate (if using non-generic pattern).
         public delegate void EventHandler(object sender, ServerEventArgs e);
         public event EventHandler OnKeyboardSelected;
        public void StartAsync();
    }
}
