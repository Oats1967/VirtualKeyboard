using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Services;

namespace VirtualKeyboard.ViewModels
{
  
    public class NumericKeyboardViewModel : KeyboardViewModel
    {

      
        public NumericKeyboardViewModel(IKeyboardService keyboardService) : base(keyboardService)
        {
        }

        public override void BackspacePressed()
        {
            _keyboardService.SendKey(0x08);
        }

        public override void KeyPressed(string key)
        {
            _keyboardService.SendKey(Convert.ToChar(key));
        }
    }
}
