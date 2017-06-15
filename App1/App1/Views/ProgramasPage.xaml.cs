using App1.Services;
using Plugin.LocalNotifications;
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
    public partial class ProgramasPage : ContentPage
    {
        public ProgramasPage()
        {
            InitializeComponent();
            BindingContext = new ProgramasPageViewModel();
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var obj = e.SelectedItem as ItemDetails;
            var answer = await DisplayAlert("Levantate Chévere", "Ver noticia completa.", "Si", "No");
            if (answer)
                Device.OpenUri(new Uri(obj.Link));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

    }



    class ProgramasPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ItemDetails> Items
        {
            get
            {
                return Settings.ListProgramas ?? new ObservableCollection<ItemDetails>();
            }
            set
            {
                Settings.ListProgramas = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Grouping<string, Item>> ItemsGrouped { get; set; }

        public ProgramasPageViewModel()
        {
            if (Settings.ListProgramas == null)
            {
                Items = new ObservableCollection<ItemDetails>(new[]
                   {
                        new ItemDetails { Text = "Desliza y Actualiza noticias Chéveres", Detail = "", ImageUrl = "Down.png" },
                     });
                Settings.ListProgramas = Items;
            }

            RefreshDataCommand = new Command(
                async () => await GetFeedPrograms());
        }

        public ICommand RefreshDataCommand { get; }

        async Task GetFeedPrograms()
        {
            IsBusy = true;

            string url = "http://levantatechevere.es/category/programas/feed/";
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
            var query = XDocument.Parse(responseString)
                         .Descendants("item")
                         .Select(i => new ItemDetails
                         {
                             Text = (string)i.Element("title"),
                             Detail = Convert.ToDateTime((string)i.Element("pubDate")).ToString("dd/MM/yyyy"),
                             //Detail = (string)i.Element("description"),
                             //Link = (string)i.Element("link"),
                         });

            if (Items.Count <= 1) Items.Clear();
            if (query.Count() > Items.Count)
            {
                foreach (var item in query)
                    Items.Add(item);
                Settings.ListProgramas = Items;
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


        /*
        bool busy;
        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        */
        public class Item
        {
            public string Text { get; set; }
            public string Detail { get; set; }

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
