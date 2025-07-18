using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Pages;

namespace VirtualKeyboard.Commands
{
    public class TkSetSize 
    {
        public TkSetSize(int layout, int percentage)
        {
            Layout = Enum.IsDefined(typeof(Layouts), layout)
             ? (Layouts)layout
             : Layouts.NotUsed;
            Percentage = Math.Clamp(percentage, 0, 100);
        }

        public int Percentage { get; }
        
        public Layouts Layout { get; }

    }
}
