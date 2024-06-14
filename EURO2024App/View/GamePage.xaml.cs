using EURO2024App.Services;
using EURO2024App.ViewModels;
using System.ComponentModel;

namespace EURO2024App.View;

public partial class GamePage : ContentPage
{
	public GamePage(GamesViewModel gamesViewModel)
	{
		InitializeComponent();
		BindingContext = gamesViewModel;
		
		
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is GamesViewModel viewModel)
        {
           await viewModel.ReloadGamesCommand.ExecuteAsync(null);
        }
    }
}