namespace EURO2024App.View;
using EURO2024App.ViewModels;
using System.ComponentModel;

public partial class GamesPage : ContentPage
{
	

    public GamesPage(GamesViewModel gamesViewModel)
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