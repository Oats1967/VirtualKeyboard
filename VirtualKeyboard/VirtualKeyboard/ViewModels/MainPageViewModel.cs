using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Services;

namespace VirtualKeyboard.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(ITCPService tcpService) : base(tcpService)
        {
        }
    }
}
