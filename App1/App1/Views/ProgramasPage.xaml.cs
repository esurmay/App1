using LevantateChevere.Services;
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

namespace LevantateChevere.Views
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
            await Navigation.PushAsync(new DetallesPage(obj.Encoded));
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

        public ProgramasPageViewModel()
        {
            if (Settings.ListProgramas == null)
            {
                Items = new ObservableCollection<ItemDetails>(new[]
                   {
                        new ItemDetails { Text = "", Detail = "", ImageUrl = "Pulldown.png" },
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
            //string url = "levantatechevere.es/category/programas/feed/";
            await Task.Run(() => Settings.GetFeedsAsync("Programas", url, Items));
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

            public override string ToString() => Text;
        }

    }
}
