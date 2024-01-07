using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Pages;
using VirtualKeyboard.Services;

namespace VirtualKeyboard.ViewModels
{
    public abstract partial class KeyboardViewModel : BaseViewModel, IRecipient<TKSetSize>, IRecipient<TKSetShowPoint> 
    {
        protected readonly IKeyboardService _keyboardService;
        protected abstract int InitWidth { get;  }
        protected abstract int InitHeight { get;  }

        protected abstract int InitX { get; }
        protected abstract int InitY { get;  }
        public Layouts Layout { get; set; }

        protected abstract (double current, double max) SizeRef { get; }

        public KeyboardViewModel(IKeyboardService service)
        {
            Width = InitWidth;
            Height = InitHeight;
            X = InitX;
            Y = InitY;
            _keyboardService = service;
            WeakReferenceMessenger.Default.RegisterAll(this);
            
        }


        [ObservableProperty]
        private int x;
        [ObservableProperty]
        private int y;

        [ObservableProperty]
        private int width;

        [ObservableProperty]
        private int height;

        [ObservableProperty]
        double fontSize;

        [ObservableProperty]
        double keySpacing;


        public void SizeChanged(object? sender, EventArgs e)
        {
            
            var fontSize = ResolutionConfig.ResolutionToFontSize[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];
            var keySpacing = ResolutionConfig.ResolutionToSpacing[(DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Height)];

            FontSize = ValueScaler.MapLinear(SizeRef.current, 0, SizeRef.max, fontSize.alphaMin, fontSize.alphaMax);
            KeySpacing = ValueScaler.MapLinear(SizeRef.current, 0, SizeRef.max, keySpacing.alphaMin, keySpacing.alphaMax);
            
        }

        public void NavigatedTo(object? sender, NavigatedToEventArgs e)
        {
           
            WindowSizeService.ResizeWindow(X, Y, Width, Height);
        }

        public void NavigatedFrom(object? sender, NavigatedFromEventArgs e)
        {
           
            WindowSizeService.ResizeWindow(0, 0, 0, 0);
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
        public abstract void BackspacePressed();
        public abstract void KeyPressed(string key);

        public void Receive(TKSetSize message)
        {
            if (!message.Layout.Equals(Layout)) return;
            Width = message.Width;
            Height = message.Height;
            CorrectOutOfBounds();
            if (Shell.Current!.CurrentPage is GermanKeyboardPage)
            {
                WindowSizeService.ResizeWindow(X, Y, Width, Height);
            }
        }

        public void Receive(TKSetShowPoint message)
        {
            if (!message.Layout.Equals(Layout)) return;
            X = message.X;
            Y = message.Y;
            CorrectOutOfBounds();
            if (Shell.Current!.CurrentPage is GermanKeyboardPage)
            {
                WindowSizeService.ResizeWindow(X, Y, Width, Height);
            }
        }

        private void CorrectOutOfBounds()
        {
            X = CorrectDimension(X, Width, (int)DeviceDisplay.MainDisplayInfo.Width);
            Y = CorrectDimension(Y, Height, (int)DeviceDisplay.MainDisplayInfo.Height);
        }

        private int CorrectDimension(int coordinate, int size, int screenSize)
        {
            var endCoordinate = coordinate + size;

            if (endCoordinate > screenSize)
            {
                return Math.Max(0, coordinate - (endCoordinate - screenSize));
            }
            return coordinate;
        }
    }
}
