using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesPage : ContentPage
    {
        public DetallesPage()
        {
            InitializeComponent();
            BindingContext = new DetallesPageViewModel();


        }
    }

    class DetallesPageViewModel : INotifyPropertyChanged
    {

        public DetallesPageViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);

        }

        public DetallesPageViewModel(string urlVideo)
        {
            IncreaseCountCommand = new Command(IncreaseCount);
            //UrlVideo = urlVideo;

            var htmlSource = new HtmlWebViewSource();
            string HTML = @"<html>
                              <body>
                                <iframe frameborder='0' src='https://www.youtube.com/embed/XGSy3_Czz8k?autoplay=1'>
                                </iframe>
                              </body>
                            </html> ";

            htmlSource.Html = HTML;
           // LNBrowser.Source = htmlSource;
        }


        int count;

        string countDisplay = "You clicked 0 times.";
        string _htmlSourceVideo = string.Empty;
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }
        public string HtmlSourceVideo
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
