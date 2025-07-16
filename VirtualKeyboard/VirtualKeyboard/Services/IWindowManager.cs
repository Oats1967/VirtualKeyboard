using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{
    public interface IWindowManager
    {
        public void Setup(MauiAppBuilder builder);

        public void ResizeWindow(int x, int y, int width, int height, int cornerRadius);
    }
}
