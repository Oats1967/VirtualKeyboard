using VirtualKeyboard.Services;
using Microsoft.Extensions.Logging;


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

        private void TcpService_OnKeyboardSelected(object sender, ServerEventArgs e)
        {
            _logger.LogInformation($"Window should be resized and {e.Text} should be activated");
        }
    }
}
