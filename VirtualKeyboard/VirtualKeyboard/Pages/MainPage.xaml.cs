using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Pages;
using VirtualKeyboard.Services;
using VirtualKeyboard.Services.Commands;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard
{
    public partial class MainPage : ContentPage, IRecipient<TKSetShow>, IRecipient<TKSetHide>
    {


       // for dependency injection Pages have to be initialized through constructor
        public MainPage(MainPageViewModel viewModel, NumericKeyboardPage numericKeyboardPage, GermanKeyboardPage germanKeyboardPage)
        {
            WeakReferenceMessenger.Default.Register<TKSetHide>(this);
            WeakReferenceMessenger.Default.Register<TKSetShow>(this);
            InitializeComponent();
            BindingContext = viewModel;
        }
       
        protected override void OnAppearing()
        {
            WindowSizeService.ResizeWindow(0, 0, 0, 0);

            base.OnAppearing();
        }

        // toDo make private 
        private async Task OpenNumericKeyboardAsync()
        {
            await Shell.Current.GoToAsync(nameof(NumericKeyboardPage));
        }

        private async Task CloseKeyboardAsync()
        {
            await Shell.Current.GoToAsync("..");
        }


        // toDo make private
        private async Task OpenGermanKeyboardAsync()
        {
            await Shell.Current.GoToAsync(nameof(GermanKeyboardPage));
        }


        async void IRecipient<TKSetShow>.Receive(TKSetShow message)
        {
            if (Shell.Current.CurrentPage is not MainPage) return;
            switch (message.Layout)
            {
                case Services.Commands.Layout.Numeric:
                    await OpenNumericKeyboardAsync();
                    break;
                case Services.Commands.Layout.German:
                    await OpenGermanKeyboardAsync();
                    break;
                default: return;
            }
        }

        async void IRecipient<TKSetHide>.Receive(TKSetHide message)
        {
            if (Shell.Current.CurrentPage is MainPage) return;
             await CloseKeyboardAsync();
            
        }
    }

}
