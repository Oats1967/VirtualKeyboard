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
            switch (e.Text)
            {
                case "NK":
                    OpenNumericKeyboard();
                    const int width = 400;
                    const int height = 600;
                    WindowSizeService.ResizeWindow(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height);
                    break;
                default: break;
            }
                      
            
          
            _logger.LogInformation($"Window should be resized and {e.Text} should be activated");
        }

        private async void OpenNumericKeyboard()
        {
            await Shell.Current.GoToAsync(nameof(NumericKeyboardPage));
        }
    }
}
