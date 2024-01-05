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
    public class MainPageViewModel : BaseViewModel 
    {

        private ILogger _logger;
      


        public MainPageViewModel(ITCPService tcpService, ILogger<MainPageViewModel> logger)
        {
            _logger = logger;
            


        }

       

        
           
    }
}
