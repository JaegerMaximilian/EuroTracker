using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EURO2024App.Services;
using EURO2024App.View;

namespace EURO2024App.ViewModels
{
    [QueryProperty(nameof(Game), "Game")]
    public partial class EventViewModel : BaseViewModel
    {
        EuroAPIService euroAPIservice;
       

        public EventViewModel(EuroAPIService euroAPIService)
        {
            Title = "Spiel";
            this.euroAPIservice = new EuroAPIService();
            
            
        }

        [ObservableProperty]
        Spiel _game;


       

    [RelayCommand]
        public async Task AddEvent()
        {
            await Shell.Current.GoToAsync(nameof(AddEventPage),true, new Dictionary<string, object>
            {
                { "Game", Game}
            });
        }

        [RelayCommand]
        public async Task BackToGames()
        {
            
            await Shell.Current.GoToAsync(nameof(GamesPage),true);
        }

        [RelayCommand]
        public async Task Refresh(Spiel game)
        {
            _game = await euroAPIservice.GetSpiel(game.Id);
            _game.GruppeString = _game.Nationen.ElementAtOrDefault(0).Gruppe.Name;
            foreach (var nation in _game.Nationen)
            {
                nation.ToreImSpiel = nation.TorEreginisse.Count(e => e.SpielId == _game.Id);
            }
            _game.Ereignisse = _game.Ereignisse.OrderByDescending(e => e.Minute).ToList();
            IsBusy = false;
        }


    }
}
