using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Commands;
using VirtualKeyboard.Services;
using VirtualKeyboard.ViewModels;




namespace VirtualKeyboard.Pages;


public partial class DutchKeyboardPage : ContentPage 
{
    public DutchKeyboardPage(AlphabeticKeyboardViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
        viewModel.Layout = Layouts.Dutch;
        SizeChanged += viewModel.SizeChanged;
        NavigatedTo += viewModel.NavigatedTo;
       
        NavigatedFrom += viewModel.NavigatedFrom;
	}


}