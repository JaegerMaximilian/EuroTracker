using EURO2024App.ViewModels;

namespace EURO2024App.View;

public partial class AddEventPage : ContentPage
{
	public AddEventPage(AddEventViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AddEventViewModel viewModel)
        {
            await viewModel.LoadEreignisTypenCommand.ExecuteAsync(null);
        }



    }
}