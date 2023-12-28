using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Services;

namespace VirtualKeyboard.ViewModels
{
  
    public class NumericKeyboardViewModel : BaseViewModel
    {

      
        public NumericKeyboardViewModel(ITCPService tcpService) : base(tcpService)
        {
        }


    }
}
