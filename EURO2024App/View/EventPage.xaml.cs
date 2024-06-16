using EURO2024App.ViewModels;

namespace EURO2024App.View;

public partial class EventPage : ContentPage
{
    public EventPage(EventViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is EventViewModel viewModel)
        {
            await viewModel.RefreshCommand.ExecuteAsync(null);
        }

    }


}
