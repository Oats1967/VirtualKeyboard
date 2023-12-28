using VirtualKeyboard.Services;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Pages;


namespace VirtualKeyboard.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        private ILogger _logger;
        public MainPageViewModel(ITCPService tcpService, ILogger<MainPageViewModel> logger) : base(tcpService)
        {
            _logger = logger;
            tcpService.OnKeyboardSelected += TcpService_OnKeyboardSelected;
            tcpService.StartAsync();

            


        }

        private async void TcpService_OnKeyboardSelected(object sender, ServerEventArgs e)
        {
            if (Shell.Current.CurrentPage is not MainPage) return;
            int width;
            int height;
            switch (e.Text)
            {
                case "NK":
                     width = 400;
                     height = 600;
                    OpenNumericKeyboardAsync(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height);
                    break;
                case "AK":
                     width = 1000;
                     height = 400;
                    OpenAlphabeticKeyboardAsync(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height);
                    break;
                default: break;
            }
                      
            
          
            _logger.LogInformation($"Window should be resized and {e.Text} should be activated");
        }


        // toDo make private 
        public async void OpenNumericKeyboardAsync(int x, int y , int width, int height)
        {
            await Shell.Current.GoToAsync($"{nameof(NumericKeyboardPage)}?Size={x},{y},{width},{height}");
        }


        // toDo make private
        public async void OpenAlphabeticKeyboardAsync(int x, int y, int width, int height)
        {
            await Shell.Current.GoToAsync($"{nameof(AlphabeticKeyboardPage)}?Size={x},{y},{width},{height}");
        }
    }
}
