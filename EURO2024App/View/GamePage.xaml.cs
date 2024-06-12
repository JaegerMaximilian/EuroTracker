using EURO2024App.Services;
using EURO2024App.ViewModels;

namespace EURO2024App.View;

public partial class GamePage : ContentPage
{
	public GamePage(SpieleViewModel spieleViewModel)
	{
		InitializeComponent();
		BindingContext = spieleViewModel;
		
		
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is GamesViewModel viewModel)
        {
            //await viewModel.ReloadTeamsCommand.ExecuteAsync(null);
        }
    }
}