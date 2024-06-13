using EURO2024App.Services;
using EURO2024App.ViewModels;

namespace EURO2024App.View;

public partial class GruppenPage : ContentPage
{
	public GruppenPage(GruppenViewModel gruppenViewModel)
	{
		InitializeComponent();
		BindingContext = gruppenViewModel;
		
		
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is GruppenViewModel viewModel)
        {
            //await viewModel.ReloadTeamsCommand.ExecuteAsync(null);
        }
    }
}