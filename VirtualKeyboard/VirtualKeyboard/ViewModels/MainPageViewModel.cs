using Microsoft.Extensions.Logging;




namespace VirtualKeyboard.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    { 
        private ILogger _logger;
        public MainPageViewModel( ILogger<MainPageViewModel> logger)
        {
            _logger = logger;
        }   
    }
}
