using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Commands;

namespace VirtualKeyboard.Services
{
    public interface IWindowService : IRecipient<TKSetShow>, IRecipient<TKSetHide>, IRecipient<TKSetShowPoint>, IRecipient<TKSetSize>
    {
        public void ResizeWindow(int x, int y, int width, int height, int cornerRadius = 16);
    }
}
