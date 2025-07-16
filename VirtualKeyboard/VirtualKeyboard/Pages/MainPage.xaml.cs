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
        }

       
    }


}
