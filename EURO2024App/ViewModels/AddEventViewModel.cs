using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EURO2024App.Services;
using EURO2024App.View;
using EURO2024App.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EURO2024App.ViewModels
{
    [QueryProperty(nameof(Game), "Game")]
    public partial class AddEventViewModel : BaseViewModel
    {
        private readonly EuroAPIService euroAPIService;

        public ObservableCollection<EreignisTyp> EreignisTypen { get; } = new();

        public AddEventViewModel(EuroAPIService euroAPIService)
        {
            this.euroAPIService = euroAPIService;
        }

        [ObservableProperty]
        private Spiel game;

        [ObservableProperty]
        private int minute;

        [ObservableProperty]
        private string kommentar;

        [ObservableProperty]
        private EreignisTyp selectedEreignisTyp;

        [ObservableProperty]
        private Nation selectedNation;

        partial void OnSelectedNationChanged(Nation value)
        {
            LoadPlayersForSelectedNation();
        }

        [ObservableProperty]
        private Spieler selectedSpieler;

        public ObservableCollection<Spieler> Players { get; } = new();

        [ObservableProperty]
        private bool isPlayerPickerVisible;

        [RelayCommand]
        public async void CreateEreignis()
        {
            try
            {
                var ereignis = CreateEreignisForAPI();
                await euroAPIService.CreateEvent(ereignis);

                Spiel spiel = await euroAPIService.GetSpiel(ereignis.SpielId);
                foreach (var nation in spiel.Nationen)
                {
                    nation.ToreImSpiel = nation.TorEreginisse.Count(e => e.SpielId == spiel.Id);
                }

                await Shell.Current.GoToAsync(nameof(EventPage), true, new Dictionary<string, object>
                {
                    { "Game", Game }
                });
            }
            catch
            {
                Spiel spiel = await euroAPIService.GetSpiel(Game.Id);

                foreach (var nation in spiel.Nationen)
                {
                    nation.ToreImSpiel = nation.TorEreginisse.Count(e => e.SpielId == spiel.Id);
                }

                await Shell.Current.GoToAsync(nameof(EventPage), true, new Dictionary<string, object>
                {
                    { "Game", Game }
                });
            }
        }

        private Ereignis CreateEreignisForAPI()
        {
            var spielereignis = new Ereignis
            {
                Minute = Minute,
                Kommentar = Kommentar,
                SpielId = Game.Id
            };

            if (SelectedNation != null)
            {
                spielereignis.TorNationId = SelectedNation.Id;
            }

            if (SelectedEreignisTyp != null)
            {
                spielereignis.EreignisTypId = SelectedEreignisTyp.Id;
            }
            if (SelectedSpieler != null)
            {
                spielereignis.TorschuetzeId = SelectedSpieler.Id;
            }

            return spielereignis;
        }

        [RelayCommand]
        public async Task LoadEreignisTypen()
        {
            List<EreignisTyp> ereignisTypen = await euroAPIService.GetEreignisTypen();

            foreach (EreignisTyp ereignisTyp in ereignisTypen)
            {
                EreignisTypen.Add(ereignisTyp);
            }
        }

        [RelayCommand]
        public async Task Back()
        {
            await Shell.Current.GoToAsync(nameof(EventPage), true, new Dictionary<string, object>
            {
                { "Game", Game }
            });
        }

        private void LoadPlayersForSelectedNation()
        {
            Players.Clear();

            if (SelectedNation != null)
            {
                foreach (var player in SelectedNation.Spieler)
                {
                    Players.Add(player);
                }
            }

            IsPlayerPickerVisible = Players.Count > 0;
        }
    }
}
