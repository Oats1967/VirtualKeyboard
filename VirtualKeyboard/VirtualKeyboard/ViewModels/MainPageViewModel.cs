using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Controls;
using VirtualKeyboard.Platforms.Windows;
using VirtualKeyboard.Services;




namespace VirtualKeyboard.ViewModels
{
    public partial class MainPageViewModel : ObservableObject, IRecipient<Layouts>
    {
        

        [ObservableProperty]
        private Layouts layout;

        private IKeyboardService _keyboardService;
        private ILogger _logger;



        public MainPageViewModel(ILogger<MainPageViewModel> logger, IKeyboardService keyboardService)
        {
            _logger = logger;
           
            _keyboardService = keyboardService;
        

            // Default initialization
            Layout = Layouts.Numeric;
            
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public void Receive(Layouts layout)
        {
            Layout = layout;
        }


        #region Keyboard - Control Behaviour

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
            ResetCapsLockIfNotLocked();
            _keyboardService.SendKey(Convert.ToChar(key));
        }

        [RelayCommand]
        public void BackspacePressed()
        {
            ResetCapsLockIfNotLocked();
            _keyboardService.SendKey(0x08);
        }

        private void ResetCapsLockIfNotLocked()
        {
            if (!Locked)
            {
                CapsLock = false;
            }
        }

        #endregion

        // Windowsize


        
    }
}
