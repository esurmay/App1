using LevantateChevere.Services;
using NodaTime;
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
        DateTime dateTime;

        public RadioVivoPageViewModel()
        {
            DateTime HoraProgramaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 02, 0);
            DateTime HoraProgramaFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 05, 0);

            var htmlSource = new HtmlWebViewSource();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                var systemInstant = SystemClock.Instance.Now;
                var MadridDateTime = systemInstant.InZone(DateTimeZoneProviders.Tzdb["Europe/Madrid"]).LocalDateTime;

                this.DateTimeMadrid = new DateTime(MadridDateTime.Year,
                                                    MadridDateTime.Month,
                                                    MadridDateTime.Day,
                                                    MadridDateTime.Hour,
                                                    MadridDateTime.Minute,
                                                    MadridDateTime.Second);

                if (CrossConnectivity.Current.IsConnected)
                {
                    if (HTMLSource == null && HoraProgramaInicio.Minute == DateTimeMadrid.Minute)
                    {
                        string HTML = @"<div style='text-align:center;width:100%'>
                                     <audio width='100%' controls='' autoplay='' name='media'><source src='http://usa1.usastreams.com:8000/tropical' type='audio/mpeg'></audio>
                                </div> ";

                        htmlSource.Html = HTML;
                        HTMLSource = htmlSource;
                    }
                    else if (DateTimeMadrid.Minute == HoraProgramaFin.Minute)
                    {
                        string HTML = string.Empty;

                        htmlSource.Html = HTML;
                        HTMLSource = htmlSource;
                    }
                }
                else
                    NoConectado = true;

                return true;
            });
        }

        public DateTime DateTimeMadrid
        {
            set
            {
                if (dateTime != value)
                {
                    dateTime = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("DateTimeMadrid"));
                    }
                }
            }
            get
            {
                return dateTime;
            }
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
