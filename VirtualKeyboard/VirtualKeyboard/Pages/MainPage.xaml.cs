using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Pages;
using VirtualKeyboard.Services;
using VirtualKeyboard.Services.Commands;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard
{
    public partial class MainPage : ContentPage, IRecipient<TKSetShow>, IRecipient<TKSetHide>
    {


       
        public MainPage(MainPageViewModel viewModel)
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
        private async void OpenNumericKeyboardAsync()
        {
            await Shell.Current.GoToAsync(nameof(NumericKeyboardPage));
        }


        // toDo make private
        private async void OpenGermanKeyboardAsync()
        {
            await Shell.Current.GoToAsync(nameof(GermanKeyboardPage));
        }


        void IRecipient<TKSetShow>.Receive(TKSetShow message)
        {
            if (Shell.Current.CurrentPage is not MainPage) return;
            switch (message.Layout)
            {
                case Services.Commands.Layout.Numeric:
                    OpenNumericKeyboardAsync();
                    break;
                case Services.Commands.Layout.German:
                    OpenGermanKeyboardAsync();
                    break;
                default: return;
            }
        }

        void IRecipient<TKSetHide>.Receive(TKSetHide message)
        {
            if (Shell.Current.CurrentPage is MainPage) return;
             Shell.Current.GoToAsync("..");
            
        }
    }

}
