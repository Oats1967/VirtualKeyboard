using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Services;
using System.Net;
using System.Net.Sockets;


namespace VirtualKeyboard.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(ITCPService tcpService) : base(tcpService)
        {
            tcpService.OnKeyboardSelected += TcpService_OnKeyboardSelected;
            tcpService.StartAsync();
        }

        private void TcpService_OnKeyboardSelected(object sender, EventArgs e)
        {
            WindowSizeService.ResizeWindow(100, 100, 100, 100);
        }
    }
}
