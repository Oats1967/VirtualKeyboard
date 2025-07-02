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
        
        SizeChanged += viewModel.SizeChanged;
        NavigatedTo += (s, e) => {
            viewModel.Layout = Layouts.English;
            viewModel.NavigatedTo(s, e);
           
        };

        NavigatedFrom += viewModel.NavigatedFrom;
	}


}