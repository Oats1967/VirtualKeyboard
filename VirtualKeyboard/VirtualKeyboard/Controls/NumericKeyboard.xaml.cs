using Controls;

namespace VirtualKeyboard.Controls;

public partial class NumericKeyboard : ContentView
{
	public NumericKeyboard(KeyboardViewModel viewModel)
	{
		BindingContext =  viewModel;
		InitializeComponent();
        Loaded += NumericKeyboard_Loaded;
	}

    private void NumericKeyboard_Loaded(object? sender, EventArgs e)
    {
        
    }
}