using System.Runtime.InteropServices;
using System.Text;
using VirtualKeyboard.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;


namespace VirtualKeyboard.ViewModels
{
    public partial class AlphabeticKeyboardViewModel : BaseViewModel
    {

        IKeyboardService _keyboardService;
        public AlphabeticKeyboardViewModel( IKeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
            Locked = false;
            CapsLock = false;
        }

        [ObservableProperty]
        private bool locked;

        [ObservableProperty]
        private bool capsLock;


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
           
            _keyboardService.SendKey(Convert.ToChar(key));
            if (!Locked)
            {
                CapsLock = false;
            }
        }

        [RelayCommand]
        public void BackspacePressed()
        {

            _keyboardService.SendKey(0x08);
            if (!Locked)
            {
                CapsLock = false;
            }
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
        }





    }
}