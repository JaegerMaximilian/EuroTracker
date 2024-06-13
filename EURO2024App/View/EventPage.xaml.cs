using EURO2024App.ViewModels;

namespace EURO2024App.View;

public partial class EventPage : ContentPage
{
	public EventPage(EventViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}