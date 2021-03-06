﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChevereMaster : ContentPage
    {
        //public ListView ListView => ChevereMenuItems;
        //public ListView ListView { get { return listView; } }

        //ListView listView;

        public ChevereMaster()
        {
            InitializeComponent();
            BindingContext = new ChevereMasterViewModel();
        }



        class ChevereMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ChevereMenuItem> ChevereMenuItems { get; }
            public ChevereMasterViewModel()
            {
                ChevereMenuItems = new ObservableCollection<ChevereMenuItem>(new[]
                {
                    new ChevereMenuItem { Id = 0, Title = "Radio OnAir" },
                    new ChevereMenuItem { Id = 1, Title = "NOSOTROS" },
                    new ChevereMenuItem { Id = 2, Title = "PROGRAMAS" },
                    new ChevereMenuItem { Id = 3, Title = "NOTICIAS" },
                    new ChevereMenuItem { Id = 4, Title = "PATROCINANTES" },
                });
            }
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }
    }
}
