using App1.Services;
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

namespace App1.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioVivoPage : ContentPage
    {
        public RadioVivoPage()
        {
            InitializeComponent();
            BindingContext = new RadioVivoPageViewModel();
        }
    }

    class RadioVivoPageViewModel : INotifyPropertyChanged
    {
        public RadioVivoPageViewModel()
        {
            var htmlSource = new HtmlWebViewSource();

            if (CrossConnectivity.Current.IsConnected)
            {
                string HTML = @"<html>
                                <body>
                                    <div style='text-align:center;width:100%'>
                                          <br/>
                                          <audio width='100%' controls='' autoplay='' name='media'><source src='http://usa1.usastreams.com:8000/tropical' type='audio/mpeg'></audio>
                                         <br/>
                                         <img width='100%' height='20%' src='http://1.bp.blogspot.com/-HQbgdBWvY24/WJDCpEmS4iI/AAAAAAAAK8w/vCR05sxPZF8FfbJUe1xljmf-q_r60gxogCK4B/s1600/200.gif' alt='Levantate Chévere On Air'>
                                   </div>
                               </body>
                           </html> ";

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
