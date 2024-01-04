using System.Runtime.InteropServices;
using System.Text;
using VirtualKeyboard.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;


namespace VirtualKeyboard.ViewModels
{
    public partial class AlphabeticKeyboardViewModel : KeyboardViewModel
    {

       
        public AlphabeticKeyboardViewModel( IKeyboardService keyboardService) : base(keyboardService)
        {
           
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
        public override void KeyPressed(string key)
        {
            if (!Locked)
            {
                CapsLock = false;
            }
            _keyboardService.SendKey(Convert.ToChar(key));
           
        }

        [RelayCommand]
        public override void BackspacePressed()
        {

            if (!Locked)
            {
                CapsLock = false;
            }
            _keyboardService.SendKey(0x08);
           
        }
    }
}