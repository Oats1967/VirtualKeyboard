using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{
    public class LayoutSettings : ILayoutSettings
    {
        private Dictionary<Layouts, (int x, int y, int width, int height)> _layouts { get; }

        public LayoutSettings(Dictionary<Layouts, (int x, int y, int width, int height)> layouts)
        {
            _layouts = layouts;
        }

        public (int x, int y, int width, int height) this[Layouts layout]
        {
            get => _layouts[layout];
            set
            {
                if (!_layouts.ContainsKey(layout))
                    throw new InvalidOperationException("Cannot add new layouts.");

                _layouts[layout] = value;
            }
        }

        public bool ContainsKey(Layouts layout) => _layouts.ContainsKey(layout);
    }



}
