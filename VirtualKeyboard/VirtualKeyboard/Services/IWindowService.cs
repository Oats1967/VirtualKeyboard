using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Messages;

namespace VirtualKeyboard.Services
{
    public interface IWindowService : IRecipient<TKSetShow>, IRecipient<TKSetHide>, IRecipient<TKSetShowPoint>, IRecipient<TkSetSize>
    {
        public void ResizeWindow(int x, int y, int width, int height, int cornerRadius = 16);
    }
}
