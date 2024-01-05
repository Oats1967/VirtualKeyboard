using VirtualKeyboard.Services;

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
