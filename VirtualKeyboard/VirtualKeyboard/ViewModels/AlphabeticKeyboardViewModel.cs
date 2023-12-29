using System.Runtime.InteropServices;
using System.Text;
using VirtualKeyboard.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;


namespace VirtualKeyboard.ViewModels
{
    public partial class AlphabeticKeyboardViewModel : BaseViewModel
    {

        [ComVisible(true)]
        [DllImport("SendKeysMAUI64.dll", EntryPoint = "SendKeys", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern int SendKeys(byte arg);
        public AlphabeticKeyboardViewModel()
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
            Locked = !Locked;
            CapsLock = !CapsLock;


        }

        [RelayCommand]
        public void CapsLockPressed()
        {
            Locked = false;
            CapsLock = !CapsLock;

            
        }


        [RelayCommand]
        public void KeyPressed(string c)
        {

            var utf8Bytes = Encoding.UTF8.GetBytes(c);
            var firstByte = utf8Bytes[0];
            // Use the firstByte as needed
            SendKeys(firstByte);
            if(!Locked)
            {
                CapsLock = false;
            }
           
            if (c.Equals("{Enter}")) Shell.Current.GoToAsync("..");

        }

    }
}
