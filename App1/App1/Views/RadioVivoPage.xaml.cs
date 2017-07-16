using LevantateChevere.Services;
using Plugin.Connectivity;
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

namespace LevantateChevere.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioVivoPage : ContentPage
    {
        public RadioVivoPage()
        {
            InitializeComponent();
            BindingContext = new RadioVivoPageViewModel();
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            var source = imageSender.Source as FileImageSource;
            if (source.File == "imgFB.png")
            {
                Device.OpenUri(new Uri("https://www.facebook.com/levancheve"));
            }
            else if (source.File == "imgINS.png")
            {
                Device.OpenUri(new Uri("https://www.instagram.com/levantatechevere/"));
            }
            else if (source.File == "imgYT.png")
            {
                Device.OpenUri(new Uri("https://www.youtube.com/c/levantatechevere"));
            }
        }

    }

    class RadioVivoPageViewModel : INotifyPropertyChanged
    {
        public RadioVivoPageViewModel()
        {
            var htmlSource = new HtmlWebViewSource();

            if (CrossConnectivity.Current.IsConnected)
            {
                string HTML = @"<div style='text-align:center;width:100%'>
                                     <audio width='100%' controls='' autoplay='' name='media'><source src='http://usa1.usastreams.com:8000/tropical' type='audio/mpeg'></audio>
                                </div> ";

                htmlSource.Html = HTML;
                HTMLSource = htmlSource;
            }
            else
                NoConectado = true;

        }

        public HtmlWebViewSource HTMLSource
        {
            get
            {
                return htmlSource;
            }
            set
            {
                htmlSource = value;
                RaisePropertyChanged();
            }
        }
        public bool NoConectado
        {
            get
            {
                return _conectado;
            }
            set
            {
                _conectado = value;
                RaisePropertyChanged();
            }
        }

        protected void RaisePropertyChanged([CallerMemberName]  string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool _conectado;
        HtmlWebViewSource htmlSource;

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        
    }


}
