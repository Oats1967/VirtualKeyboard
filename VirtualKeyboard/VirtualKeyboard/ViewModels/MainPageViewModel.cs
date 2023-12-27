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
            _logger.LogInformation($"Window should be resized and {e.Text} should be activated");
           
            await Shell.Current.GoToAsync(nameof(NumericKeyboardPage));
            const int width = 1200;
            const int height = 400;
            WindowSizeService.ResizeWindow(1920 / 2 - width / 2, 1080 / 2 - height, width, height);
        }
    }
}
