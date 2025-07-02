using VirtualKeyboard.ViewModels;




namespace VirtualKeyboard.Pages;


public partial class GermanKeyboardPage : ContentPage 
{
    public GermanKeyboardPage(AlphabeticKeyboardViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;

        SizeChanged += viewModel.SizeChanged;
        NavigatedTo += (s, e) => {
            viewModel.Layout = Layouts.German;
            viewModel.NavigatedTo(s, e);

        };

        NavigatedFrom += viewModel.NavigatedFrom;
	}


}