using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using VirtualKeyboard;
using VirtualKeyboard.Commands;

using VirtualKeyboard.Services;

namespace Controls
{
    public partial class KeyboardViewModel : ObservableObject
    {

        protected ILogger _logger;

        protected readonly IKeyboardService _keyboardService;


        [ObservableProperty]
        private Layouts layout;



        // From Keyboard VM
        [ObservableProperty]
        private double fontSize;

        [ObservableProperty]
        private double keySpacing;
        //




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


        // Alpha
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
            WeakReferenceMessenger.Default.RegisterAll(this);
            
        }

        protected (double current, double max) SizeRef =>
            (Application.Current!.Windows[0].Width, DeviceDisplay.Current.MainDisplayInfo.Width);


        // Besser in Main und dann Binding iregndwie!!!
        [RelayCommand]
        public void OnLoaded()
        {
            _logger.LogInformation("OnLoaded View");
            var fontSize = ResolutionConfig.ResolutionToFontSize[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];
            var keySpacing = ResolutionConfig.ResolutionToSpacing[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];
            var fontsizeMin = Layout switch
            {
                Layouts.Numeric => fontSize.numericMin,
                _ => fontSize.alphaMin
            };
            var fontsizeMax = Layout switch
            {
                Layouts.Numeric => fontSize.numericMax,
                _ => fontSize.alphaMax
            };
            var keySpacingMin = Layout switch
            {
                Layouts.Numeric => keySpacing.numericMin,
                _ => keySpacing.alphaMin
            };
            var keySpacingMax = Layout switch
            {
                Layouts.Numeric => keySpacing.numericMax,
                _ => keySpacing.alphaMax
            };
            FontSize = ValueScaler.MapLinear(SizeRef.current, 0, SizeRef.max, fontSize.alphaMin, fontSize.alphaMax);
            KeySpacing = ValueScaler.MapLinear(SizeRef.current, 0, SizeRef.max, keySpacing.alphaMin, keySpacing.alphaMax);
        }



        //


        






    }
}
