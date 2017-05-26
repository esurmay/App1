using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChevereDetail : ContentPage
    {
        public ChevereDetail()
        {
            InitializeComponent();

            var browser = new WebView();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body style='background-color:blue;'><h1>Xamarin.Forms</h1><p>Welcome to WebView.</p></body></html>";
            browser.Source = htmlSource; // "http://levantatechevere.es/";
        }
    }
}
