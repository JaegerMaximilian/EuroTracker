using EURO2024App.ViewModels;
using EURO2024App.Services;


namespace EURO2024App.View;

public partial class StatistikPage : ContentPage
{
    public StatistikPage(StatistikViewModel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is StatistikViewModel viewModel)
        {
            await viewModel.ReloadNationenCommand.ExecuteAsync(null);
        }
    }
}