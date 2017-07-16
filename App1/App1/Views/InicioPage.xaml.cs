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
    public partial class InicioPage : ContentPage
    {
        public InicioPage()
        {
            InitializeComponent();
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



}
