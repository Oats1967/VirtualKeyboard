using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Services;

namespace VirtualKeyboard.ViewModels
{
  
    public partial class NumericKeyboardViewModel : KeyboardViewModel
    {

      
        public NumericKeyboardViewModel(IKeyboardService keyboardService) : base(keyboardService)
        {
        }
        [RelayCommand]
        public override void BackspacePressed()
        {
            _keyboardService.SendKey(0x08);
        }
        [RelayCommand]
        public override void KeyPressed(string key)
        {
            _keyboardService.SendKey(Convert.ToChar(key));
        }
    }
}
