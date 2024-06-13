using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class EventViewModel : BaseViewModel
    {
        EuroAPIService euroAPIservice;
       

        public EventViewModel(EuroAPIService euroAPIService)
        {
            Title = "Events";
            this.euroAPIservice = euroAPIservice;
        }

        [ObservableProperty]
        Spiel _game;

        

    }
}
