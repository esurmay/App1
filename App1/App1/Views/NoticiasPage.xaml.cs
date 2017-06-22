﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Services;
using Plugin.LocalNotifications;

namespace App1.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoticiasPage : ContentPage
    {
        public NoticiasPage()
        {
            InitializeComponent();
            BindingContext = new NoticiasPageViewModel();
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
          => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var obj = e.SelectedItem as ItemDetails;
            await Navigation.PushAsync(new DetallesPage(obj.Encoded));

            //var obj = e.SelectedItem as ItemDetails;
            //var answer = await DisplayAlert("Levantate Chévere", "Ver noticia completa.", "Si", "No");
            //if (answer)
            //    Device.OpenUri(new Uri(obj.Link));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

    }


    class NoticiasPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ItemDetails> Items
        {
            get
            {
                return Settings.ListNoticias ?? new ObservableCollection<ItemDetails>();
            }
            set
            {
                Settings.ListNoticias = value;
                RaisePropertyChanged();
            }
        }

        public NoticiasPageViewModel()
        {
            if (Settings.ListNoticias == null)
            {
                Items = new ObservableCollection<ItemDetails>(new[]
                   {
                        new ItemDetails { Text = "", Detail = "", ImageUrl = "Pulldown.png" },
                     });
                Settings.ListNoticias = Items;
            }

            RefreshDataCommand = new Command(
                async () => await GetFeedNews());
        }

        public ICommand RefreshDataCommand { get; }


        async Task GetFeedNews()
        {
            IsBusy = true;
            string url = "http://levantatechevere.es/category/noticias/feed/";
            await Task.Run(() => Settings.GetFeeds("Noticias", url, Items));
            IsBusy = false;
        }

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                RaisePropertyChanged();
            }
        }

        protected void RaisePropertyChanged([CallerMemberName]  string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool isBusy;

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public class Item
        {
            public string Text { get; set; }
            public string Detail { get; set; }
            public string ImageUrl { get; set; }

            public override string ToString() => Text;
        }

        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> items)
            {
                Key = key;
                foreach (var item in items)
                    this.Items.Add(item);
            }
        }


    }

}
