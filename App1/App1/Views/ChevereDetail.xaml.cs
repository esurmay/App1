using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LevantateChevere.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChevereDetail : ContentPage
    {
        public ChevereDetail()
        {
            InitializeComponent();

            var htmlSource = new HtmlWebViewSource();
            string HTML = @"<html>
                                <body>
                                    <div style='text-align:center;width:100%'>
                                       <img src='http://levantatechevere.es/wp-content/uploads/2017/04/logoWeb-e1493115155338.png' alt='Levantate Chévere On Air'>
                                          <br/>
                                          <audio width='100%' controls='' autoplay='' name='media'><source src='http://usa1.usastreams.com:8000/tropical' type='audio/mpeg'></audio>
                                         <br/>
                                         <img width='100%' height='20%' src='http://1.bp.blogspot.com/-HQbgdBWvY24/WJDCpEmS4iI/AAAAAAAAK8w/vCR05sxPZF8FfbJUe1xljmf-q_r60gxogCK4B/s1600/200.gif' alt='Levantate Chévere On Air'>
                                   </div>
                               </body>
                           </html> ";

            htmlSource.Html = HTML;
            LVBrowser.Source = htmlSource;
        }
    }
}
