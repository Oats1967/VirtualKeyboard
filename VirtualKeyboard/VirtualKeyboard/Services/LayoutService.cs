using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{
    public class LayoutService : ILayoutService
    {
        public Dictionary<Layouts, (int x, int y, int width, int height)> Dictionary { get; private set; } = new();

        public LayoutService()
        {
            // Add new Layouts here
            Dictionary = new Dictionary<Layouts, (int x, int y, int width, int height)>
            {
                [Layouts.Numeric] = (0,0,0,0),
                [Layouts.German] = (0, 0, 0, 0),
                [Layouts.English] = (0, 0, 0, 0),
                [Layouts.Dutch] = (0, 0, 0, 0),
                [Layouts.French] = (0, 0, 0, 0),
                [Layouts.Polish] = (0, 0, 0, 0),
            };
        }

       
    }


}
