using VirtualKeyboard.Services;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Pages;
using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Services.Commands;
using Layout = VirtualKeyboard.Services.Commands.Layout;
using System.Drawing;
using System.Runtime.Versioning;




namespace VirtualKeyboard.ViewModels
{
    public class MainPageViewModel : BaseViewModel , IRecipient<TKSetShow>, IRecipient<TKSetSize>, IRecipient<TKSetShowPoint>
    {

        private ILogger _logger;
        private int _x;
        private int _y;
        private double _width;
        private double _height;

       


        public MainPageViewModel(ITCPService tcpService, ILogger<MainPageViewModel> logger)
        {
            _logger = logger;
            WeakReferenceMessenger.Default.Register<TKSetShow>(this);
            WeakReferenceMessenger.Default.Register<TKSetSize>(this);
            WeakReferenceMessenger.Default.Register<TKSetShowPoint>(this);
            tcpService.StartAsync();


        }

       


        // toDo make private 
        public async void OpenNumericKeyboardAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(NumericKeyboardPage)}?WindowInfo={_x}|{_y}|{_width}|{_height}");
        }


        // toDo make private
        public async void OpenGermanKeyboardAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(GermanKeyboardPage)}?WindowInfo={_x}|{_y}|{_width}|{_height}");
        }
        public async void CloseKeyboardAsync()
        {
            if (Shell.Current.CurrentPage is not MainPage)
            {
                await Shell.Current.GoToAsync("..");
            }
        }






        void IRecipient<TKSetShow>.Receive(TKSetShow message)
        {
           CloseKeyboardAsync();
            switch (message.Layout)
            {
                case Layout.German:
                    OpenGermanKeyboardAsync();
                    break;
                case Layout.Numeric:
                    OpenNumericKeyboardAsync();
                    break;
                default:
                    break;     
            }
        }
    

        void IRecipient<TKSetSize>.Receive(TKSetSize message)
        {
            // toDo set members 
            double screenHeight = DeviceDisplay.MainDisplayInfo.Height;
            
            CloseKeyboardAsync() ;
            switch (message.Layout)
            {
                case Layout.German:
                    _height = screenHeight * ((double)message.Percentage / 100); ;
                    _width = _height / GermanKeyboardPage.Ratio;
                    OpenGermanKeyboardAsync();
                    break;
                case Layout.Numeric:
                    _height = screenHeight * ((double)message.Percentage / 100); ;
                    _width = _height / NumericKeyboardPage.Ratio; ;
                    OpenNumericKeyboardAsync();
                    break;
                case Layout.NotUsed:
                    break;
                default:
                    throw new NotImplementedException();
                    
            }
        }
    

        void IRecipient<TKSetShowPoint>.Receive(TKSetShowPoint message)
        { 
            // toDo set members 
            _x = message.X;
            _y = message.Y;

            CloseKeyboardAsync();
            switch (message.Layout)
                {
                    case Layout.German:
                        OpenGermanKeyboardAsync();
                    break;
                    case Layout.Numeric:
                        OpenNumericKeyboardAsync();
                    break;
                        case Layout.NotUsed:
                    break;
                default:
                    throw new NotImplementedException();
            }
            }
           


            
       
    }
}
