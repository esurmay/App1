using System;
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

namespace App1.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoticiasPage : ContentPage
    {
        public NoticiasPage()
        {
            InitializeComponent();
            BindingContext = new NoticiasPageViewModel(null);
            var objlist = LVNoticias.ItemsSource;
            
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
          => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return; 

            await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

    }


    class NoticiasPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<Grouping<string, Item>> ItemsGrouped { get; set; }

        public NoticiasPageViewModel(object LVNoticias)
        {
            if (LVNoticias == null || LVNoticias.Count == 0)
            {
                Items = new ObservableCollection<Item>(new[]
                {
                    new Item { Text = "Desliza chévere para ver las noticias", Detail = "", DefaultImage = "Pulldown.png" },
                }); 
            }

            //var sorted = from item in Items
            //             orderby item.Text
            //             group item by item.Text[0].ToString() into itemGroup
            //             select new Grouping<string, Item>(itemGroup.Key, itemGroup);

            //ItemsGrouped = new ObservableCollection<Grouping<string, Item>>(sorted);
           // GetFeedNewsSync();

            RefreshDataCommand = new Command(
                async () => await GetFeedNews());
        }

        public ICommand RefreshDataCommand { get; }

        void GetFeedNewsSync()
        {
            string url = "http://levantatechevere.es/category/noticias/feed/";
            string responseString = string.Empty;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    responseString = responseContent.ReadAsStringAsync().Result;
                }
            }
            responseString = responseString.TrimStart();
            var items1 = XDocument.Parse(responseString)
                          .Descendants("item")
                          .Select(i => new Item
                          {
                              Text = (string)i.Element("title"),
                              Detail = (string)i.Element("description"),
                          });
            Items.Clear();
            foreach (var item in items1)
            {
                Items.Add(item);
            }

        }


        async Task GetFeedNews()
        {
            IsBusy = true;
            //Load Data Here

            string url = "http://levantatechevere.es/category/noticias/feed/";
            string responseString = string.Empty;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    responseString = responseContent.ReadAsStringAsync().Result;
                }
            }
            responseString = responseString.TrimStart();
            var items1 = XDocument.Parse(responseString)
                         .Descendants("item")
                         .Select(i => new Item
                         {
                             Text = (string)i.Element("title"),
                             Detail = (string)i.Element("description"),
                             DefaultImage = "",
                         });
            if(Items.Count == 1) Items.Clear();
            if (items1.Count() > Items.Count)
            {
                foreach (var item in items1)
                {
                    Items.Add(item);
                } 
            }

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
            public string DefaultImage { get; set; }

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
