using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EURO2024App.Services;
using EURO2024App.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURO2024App.ViewModels
{
    [QueryProperty(nameof(Game), "Game")]
    public partial class AddEventViewModel : BaseViewModel
    {
        EuroAPIService euroAPIService;
        public ObservableCollection<EreignisTyp> EreignisTypen { get; } = new();
        public AddEventViewModel(EuroAPIService euroAPIService)
        {
            this.euroAPIService = euroAPIService;
        }

        [ObservableProperty]
        Spiel _game;

        [ObservableProperty]
        int _minute;

        [ObservableProperty]
        string _kommentar;

        [ObservableProperty]
        EreignisTyp selectedEreignisTyp;

        [ObservableProperty]
        Nation selectedNation;

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
                    { "Game", spiel }
                });
                //await Shell.Current.GoToAsync(nameof(EventPage), true);

            }
            catch
            {
                
                Spiel spiel = await euroAPIService.GetSpiel(_game.Id);

                foreach (var nation in spiel.Nationen)
                {
                    nation.ToreImSpiel = nation.TorEreginisse.Count(e => e.SpielId == spiel.Id);
                }
                await Shell.Current.GoToAsync(nameof(EventPage), true, new Dictionary<string, object>
                {
                    { "Game", spiel }
                });

            }

        }

        private Ereignis CreateEreignisForAPI()
        {
            var spielereignis = new Ereignis();
            spielereignis.Minute = Minute;
            spielereignis.Kommentar = Kommentar;
            spielereignis.SpielId = Game.Id;

            if(selectedNation != null)
            {
                spielereignis.TorNationId = selectedNation.Id;
            }
            if(selectedEreignisTyp != null)
            {
                spielereignis.EreignisTypId = selectedEreignisTyp.Id;
            }
            

            return spielereignis;
        }

        [RelayCommand]
        public async Task LoadEreignisTypen()
        {
            List<EreignisTyp> ereignisTypen = new();
            ereignisTypen = await euroAPIService.GetEreignisTypen();

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
                { "Game", Game}
            });
           
        }
    }

}
