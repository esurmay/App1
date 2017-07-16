using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.LocalNotifications;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LevantateChevere.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesPage : ContentPage
    {
        public DetallesPage()
        {
            InitializeComponent();
            BindingContext = new DetallesPageViewModel();


        }
        public DetallesPage(string contenido)
        {
            InitializeComponent();
            BindingContext = new DetallesPageViewModel(contenido);
        }
    }

    class DetallesPageViewModel : INotifyPropertyChanged
    {

        public DetallesPageViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);

        }

        public DetallesPageViewModel(string contenido)
        {
            IncreaseCountCommand = new Command(IncreaseCount);
            //UrlVideo = urlVideo;

            var htmlSource = new HtmlWebViewSource();
            string HTML = @"<html>
                               <body style='font-family:verdana; font-size:90%;'>" + 
                                contenido + 
                              "</body>" +
                           "</html> ";

            htmlSource.Html = HTML;
            HtmlSourceVideo = htmlSource;
            // LNBrowser.Source = htmlSource;
            //CrossLocalNotifications.Current.Show("Titulo", "Mensaje en Modo RELEASE. LOGRADO!!", 1, DateTime.Now.AddSeconds(10));
        }


        int count;

        string countDisplay = "You clicked 0 times.";
        HtmlWebViewSource _htmlSourceVideo;
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }
        public HtmlWebViewSource HtmlSourceVideo
        {
            get { return _htmlSourceVideo; }
            set { _htmlSourceVideo = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
