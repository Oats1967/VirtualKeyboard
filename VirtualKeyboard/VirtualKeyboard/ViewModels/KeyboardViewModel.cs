using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VirtualKeyboard.Services;

namespace VirtualKeyboard.ViewModels
{
    public abstract partial class KeyboardViewModel : BaseViewModel
    {
        protected readonly IKeyboardService _keyboardService;
        public KeyboardViewModel(IKeyboardService service)
        {
            _keyboardService = service;
        }

        [RelayCommand]
        public void LeftPressed()
        {
            _keyboardService.SendKey(0x25);
        }

        [RelayCommand]
        public void RightPressed()
        {
            _keyboardService.SendKey(0x27);
        }

        [RelayCommand]
        public void EnterPressed()
        {
            _keyboardService.SendKey(0x0D);
            Shell.Current.GoToAsync("..");
        }

      
        public abstract void BackspacePressed();
        public abstract void KeyPressed(string key);
    }
}
