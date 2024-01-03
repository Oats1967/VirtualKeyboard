using VirtualKeyboard.Pages;

namespace VirtualKeyboard
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NumericKeyboardPage), typeof(NumericKeyboardPage));
            Routing.RegisterRoute(nameof(AlphabeticKeyboardPage), typeof(AlphabeticKeyboardPage));
        }
    }
}