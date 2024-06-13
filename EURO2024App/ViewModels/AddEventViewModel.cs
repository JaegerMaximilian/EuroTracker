using CommunityToolkit.Mvvm.ComponentModel;
using EURO2024App.Services;
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
    }

}
