using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard
{
    public partial class MainPage : ContentPage
    {
        int count = 0;


        private MainPageViewModel? ViewModel => BindingContext as MainPageViewModel;
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
          
        }

        protected override void OnAppearing()
        {
            // toDo uncomment
            // WindowSizeService.ResizeWindow(0, 0, 0, 0);

            // toDo delete underneath
            const int width = 400;
            const int height = 600;
            ViewModel?.OpenNumericKeyboardAsync(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height);

            base.OnAppearing();
        }

    }

}
