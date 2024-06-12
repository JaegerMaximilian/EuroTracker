using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURO2024App.Services;


namespace EURO2024App.ViewModels
{
    public partial class BaseViewModel:ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;



        public bool IsNotBusy => !IsBusy;

        

    }
}
