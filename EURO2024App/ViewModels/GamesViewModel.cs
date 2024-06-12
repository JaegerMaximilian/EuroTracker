using CommunityToolkit.Mvvm.Input;
using EURO2024App.Services;
using EURO2024App.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURO2024App.ViewModels
{
    public partial class GamesViewModel : BaseViewModel
    {
        public ObservableCollection<Spiel> Games { get; } = new();
        EuroAPIService euroAPIservice;

        public GamesViewModel(EuroAPIService euroAPIservice)
        {
            Title = "Games";
            this.euroAPIservice = euroAPIservice;
        }

        [RelayCommand]
        async Task GetGamesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var games = await euroAPIservice.GetSpiele();

                if (Games.Count != 0)
                    Games.Clear();

                foreach (var game in games)
                    Games.Add(game);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get games: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        [RelayCommand]
        async Task GoToGameDetails(Spiel game)
        {
            await Shell.Current.GoToAsync(nameof(EventPage), true, new Dictionary<string, object>
            {
                { "Game", game }
            });
        }

        [RelayCommand]
        async Task ReloadTeams()
        {
            Games.Clear();
            await GetGamesAsync();

        }

        [RelayCommand]
        async Task AddGame()
        {
            await Shell.Current.GoToAsync(nameof(AddEventPage), true);
        }

    }
}
