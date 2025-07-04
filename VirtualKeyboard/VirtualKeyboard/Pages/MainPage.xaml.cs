using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Controls;
using VirtualKeyboard.Converter;
using VirtualKeyboard.Pages;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;

namespace VirtualKeyboard.Pages
{
    public partial class MainPage : ContentPage
    {


       // for dependency injection Pages have to be initialized through constructor
        public MainPage(MainPageViewModel viewModel, LayoutToKeyboardConverter layoutConverter)
        {
            BindingContext = viewModel;
            Resources.Add("LayoutConverter", layoutConverter);
          
            InitializeComponent();


            if (Template.Content?.BindingContext is KeyboardViewModel keyboardViewModel)
            {
                // Initial sync
                keyboardViewModel.FontSize = viewModel.FontSize;
                keyboardViewModel.KeySpacing = viewModel.KeySpacing;

                // Forward changes from MainPageViewModel to KeyboardViewModel
                viewModel.PropertyChanged += (_, e) =>
                {
                    if (e.PropertyName == nameof(viewModel.FontSize))
                        keyboardViewModel.FontSize = viewModel.FontSize;

                    if (e.PropertyName == nameof(viewModel.KeySpacing))
                        keyboardViewModel.KeySpacing = viewModel.KeySpacing;

                    if (e.PropertyName == nameof(viewModel.Layout))
                    {
                        keyboardViewModel.CapsLock = false;
                        keyboardViewModel.Locked = false;
                    }

                };

                // Optionally sync changes back from KeyboardViewModel to MainPageViewModel
                keyboardViewModel.PropertyChanged += (_, e) =>
                {
                    if (e.PropertyName == nameof(keyboardViewModel.FontSize))
                        viewModel.FontSize = keyboardViewModel.FontSize;

                    if (e.PropertyName == nameof(keyboardViewModel.KeySpacing))
                        viewModel.KeySpacing = keyboardViewModel.KeySpacing;
                };
            }
        }

       
    }


}
