using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EURO2024App.Services;
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
    public partial class StatistikViewModel : BaseViewModel
    {
        public ObservableCollection<Nation> Nationen { get; } = new();
        public ObservableCollection<Spieler> Spieler { get; } = new();



        private EuroAPIService euroAPIservice;

        public StatistikViewModel(EuroAPIService euroAPIservice)
        {

            this.euroAPIservice = euroAPIservice;
            //IsBusy = true;
        }

        [RelayCommand]
        async Task GetNationenAndSpieler()
        {

            //IsBusy = true;
            List<Gruppe> gruppen = new();
            gruppen = await euroAPIservice.GetGruppen();


            foreach (Gruppe gruppe in gruppen)
            {
                foreach (Nation nation in gruppe.Nationen)
                {
                    Nationen.Add(nation);
                    foreach (Spieler spieler in nation.Spieler)
                    {
                        Spieler.Add(spieler);
                    }
                }
            }
            Nationen.OrderByDescending(e => e.Punkte).OrderBy(e => e.Torverhältnis);
            Spieler.OrderByDescending(e => e.Tore);
        }

       


        [RelayCommand]
        async Task ReloadNationen()
        {
            Nationen.Clear();
            await GetNationenAndSpieler();
            IsBusy = false;
        }

    }
}
