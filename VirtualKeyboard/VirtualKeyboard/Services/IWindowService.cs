using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{
    public interface IWindowService
    {
        

        public void ResizeWindow(int x, int y, int width, int height, int cornerRadius = 16);
    }
}
