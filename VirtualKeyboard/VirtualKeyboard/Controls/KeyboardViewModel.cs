using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using VirtualKeyboard;

using VirtualKeyboard.Services;

namespace VirtualKeyboard.Controls
{
    public partial class KeyboardViewModel : ObservableObject
    {

        protected ILogger _logger;

        protected readonly IKeyboardService _keyboardService;


       

        [ObservableProperty]
        private double fontSize;

        [ObservableProperty]
        private double keySpacing;

       
        [ObservableProperty]
        private bool locked;

        [ObservableProperty]
        private bool capsLock;





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

        }


        [RelayCommand]
        public void Lock()
        {

            CapsLock = !CapsLock;
            Locked = CapsLock;
        }

        [RelayCommand]
        public void CapsLockPressed()
        {
            Locked = false;
            CapsLock = !CapsLock;
        }

        [RelayCommand]
        public void KeyPressed(string key)
        {
            if (!Locked)
            {
                CapsLock = false;
            }
            _keyboardService.SendKey(Convert.ToChar(key));
        }

        [RelayCommand]
        public void BackspacePressed()
        {
            if (!Locked)
            {
                CapsLock = false;
            }
            _keyboardService.SendKey(0x08);
        }


        public KeyboardViewModel(IKeyboardService service, ILogger<KeyboardViewModel> logger) 
        {
            _logger = logger;
            _keyboardService = service;
        }



    }
}
