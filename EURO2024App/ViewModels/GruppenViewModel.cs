﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class GruppenViewModel : BaseViewModel
    {
        public ObservableCollection<Gruppe> Gruppen { get; } = new();

        [ObservableProperty]
        private bool _isRefreshing;

        private EuroAPIService euroAPIservice;

        public GruppenViewModel(EuroAPIService euroAPIservice)
        {
            Title = "Gruppen";
            this.euroAPIservice = euroAPIservice;
            //IsBusy = true;
        }

        [RelayCommand]
        async Task GetGruppenAsync()
        {

            try
            {
                IsRefreshing = true;
                List<Gruppe> gruppen = new();
                gruppen = await euroAPIservice.GetGruppen();

                foreach (Gruppe gruppe in gruppen)
                {

                    gruppe.Nationen = gruppe.Nationen
                        .OrderByDescending(e => e.Punkte)
                        .ThenByDescending(e => e.Torverhältnis)
                        .ToList(); 
                    Gruppen.Add(gruppe);
                }
                OnPropertyChanged(nameof(Gruppen));

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
        async Task ReloadGruppen()
        {
            Gruppen.Clear();
            await GetGruppenAsync();
            IsBusy = false;
        }

    }
}
