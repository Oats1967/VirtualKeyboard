using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
          
        }

        protected override void OnAppearing()
        {
            WindowSizeService.ResizeWindow(0, 0, 0, 0);
            base.OnAppearing();
        }

    }

}
