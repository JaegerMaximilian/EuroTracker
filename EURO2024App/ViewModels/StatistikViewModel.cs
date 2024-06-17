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

        private readonly EuroAPIService euroAPIservice;

        public StatistikViewModel(EuroAPIService euroAPIservice)
        {
            this.euroAPIservice = euroAPIservice;
            //IsBusy = true;
        }

        [RelayCommand]
        async Task GetNationenAndSpieler()
        {
            //IsBusy = true;
            List<Gruppe> gruppen = await euroAPIservice.GetGruppen();

            List<Nation> tempNationen = new();
            List<Spieler> tempSpieler = new();

            foreach (Gruppe gruppe in gruppen)
            {
                foreach (Nation nation in gruppe.Nationen)
                {
                    tempNationen.Add(nation);
                    foreach (Spieler spieler in nation.Spieler)
                    {
                        tempSpieler.Add(spieler);
                    }
                }
            }

            // Sort the collections
            var sortedNationen = tempNationen
                .OrderByDescending(e => e.Punkte)
                .ThenByDescending(e => e.Torverhältnis)
                .ToList();

            var sortedSpieler = tempSpieler
                .OrderByDescending(e => e.Tore)
                .ToList();

            // Clear the existing collections and add the sorted items
            Nationen.Clear();
            foreach (var nation in sortedNationen)
            {
                Nationen.Add(nation);
            }

            Spieler.Clear();
            foreach (var spieler in sortedSpieler)
            {
                Spieler.Add(spieler);
            }
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
