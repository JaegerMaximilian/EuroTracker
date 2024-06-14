using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EURO2024App.Services;
using EURO2024App.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EURO2024App.ViewModels
{
    [QueryProperty(nameof(Game), "Game")]
    public partial class AddEventViewModel : BaseViewModel
    {
        EuroAPIService euroAPIService;
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
        int _torNationId;

        [RelayCommand]
        public async void CreateEreignis()
        {
            try
            {
                var ereignis = CreateEreignisForAPI();
                await euroAPIService.CreateEvent(ereignis);
                await Shell.Current.GoToAsync(nameof(EventPage), true);

            }
            catch
            {
                //Shell.Current.ShowPopup(new ErrorPopupView(ex, "Error Adding Event"));

            }

        }

        private Ereignis CreateEreignisForAPI()
        {
            var spielereignis = new Ereignis();
            spielereignis.Minute = Minute;
            spielereignis.Kommentar = Kommentar;
            spielereignis.SpielId = Game.Id;
            spielereignis.TorNationId = TorNationId;

            return spielereignis;
        }
    }

}
