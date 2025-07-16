using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{
    public class LayoutService : ILayoutService
    {
        public Dictionary<Layouts, (int x, int y, int width, int height)> Layouts { get; }

        public LayoutService(Dictionary<Layouts, (int x, int y, int width, int height)> layouts)
        {
            Layouts = layouts;
        }
    }



}
