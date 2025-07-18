using Microsoft.UI;
using VirtualKeyboard.Services;
using Windows.Graphics;

namespace VirtualKeyboard
{

    public partial class App : Application
    {
        
        // is this necessary)
        public App(ITCPService tcpService , IWindowService windowService,  AppShell shell)
        {
            InitializeComponent();
            MainPage = shell;

            

        }

        


    }
}
