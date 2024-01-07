using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Pages;
using VirtualKeyboard.Services;




namespace VirtualKeyboard.ViewModels
{
    public class MainPageViewModel : BaseViewModel, IRecipient<TKSetShow>, IRecipient<TKSetHide>, IRecipient<TKSetShowPoint>
    { 
        private ILogger _logger;
        public MainPageViewModel( ILogger<MainPageViewModel> logger)
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
            _logger = logger;
        }

        public void Appearing(object? sender, EventArgs e)
        {
            WindowSizeService.ResizeWindow(0, 0, 0, 0);
        }


        async void IRecipient<TKSetShow>.Receive(TKSetShow message)
        {
            if (Shell.Current.CurrentPage is not MainPage)
            {
                await CloseKeyboardAsync();
            };
            switch (message.Layout)
            {
                case Layouts.Numeric:
                    await OpenNumericKeyboardAsync();
                    break;
                case Layouts.German:
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

        async void IRecipient<TKSetShowPoint>.Receive(TKSetShowPoint message)
        {
            if (Shell.Current.CurrentPage is not MainPage) return;
            switch (message.Layout)
            {
                case Layouts.Numeric:
                    await OpenNumericKeyboardAsync();
                    break;
                case Layouts.German:
                    await OpenGermanKeyboardAsync();
                    break;
                default: return;
            }
        }


        
        private async Task OpenNumericKeyboardAsync()
        {
            await Shell.Current.GoToAsync(nameof(NumericKeyboardPage));
        }
        private async Task CloseKeyboardAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
        private async Task OpenGermanKeyboardAsync()
        {
            await Shell.Current.GoToAsync(nameof(GermanKeyboardPage));
        }
    }
}
