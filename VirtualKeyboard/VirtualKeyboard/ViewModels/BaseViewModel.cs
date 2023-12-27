using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Services;

namespace VirtualKeyboard.ViewModels
{
    public class BaseViewModel
    {
        private  ITCPService _tcpService;
        public BaseViewModel(ITCPService tcpService)
        {
            _tcpService = tcpService;   
        }
    }
}
