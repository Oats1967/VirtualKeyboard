using Microsoft.UI;
using VirtualKeyboard.Services;
using Windows.Graphics;

namespace VirtualKeyboard
{

    public partial class App : Application
    {
        
        
        public App(ITCPService tcpService)
        {
            InitializeComponent();
           
            MainPage = new AppShell();

            

        }

        


    }
}
