using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;




namespace VirtualKeyboard.Pages;


public partial class EnglishKeyboardPage : ContentPage 
{
    public EnglishKeyboardPage(AlphabeticKeyboardViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
        viewModel.Layout = Layouts.English;
        SizeChanged += viewModel.SizeChanged;
        NavigatedTo += viewModel.NavigatedTo;
       
        NavigatedFrom += viewModel.NavigatedFrom;
	}


}