using CommunityToolkit.Mvvm.ComponentModel;
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

        [ObservableProperty]
        private bool _isRefreshing;

        private EuroAPIService euroAPIservice;

        public GamesViewModel(EuroAPIService euroAPIservice)
        {
            //IsRefreshing = true;
            Title = "Games";
            this.euroAPIservice = euroAPIservice;
        }

        [RelayCommand]
        async Task GetGamesAsync()
        {

            try
            {
                IsRefreshing = true;
                List<Spiel> games = new();
                games = await euroAPIservice.GetSpiele();
                games.OrderBy(game => game.Datetime); //.OrderBy(game => game.Nationen.ElementAtOrDefault(0).Gruppe).ToList();

                foreach (Spiel game in games)
                {
                    game.GruppeString = game.Nationen.ElementAtOrDefault(0).Gruppe.Name;
                    if(game.Ereignisse != null)
                    {
                        if (game.Ereignisse.Count() != 0)
                        {
                            foreach (var nation in game.Nationen)
                            {
                                nation.ToreImSpiel = nation.TorEreginisse.Count(e => e.SpielId == game.Id);
                            }
                        }
                    }
                    
                    Games.Add(game);
                }

            }
            catch (Exception ex)
            {
                //Shell.Current.ShowPopup(new ErrorPopupView(ex, "Error Loading Games"));
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
        async Task ReloadGames()
        {
            Games.Clear();
            await GetGamesAsync();
            IsBusy = false;

        }

    }
}
