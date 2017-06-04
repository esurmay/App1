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
    public partial class Chevere : MasterDetailPage
    {
        public Chevere()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

        }

        private void ListView_ItemSelected1(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as ChevereMenuItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                MasterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as ChevereMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            Detail = new NavigationPage(page);
            //var nav = new NavigationPage(page);
            //nav.BarBackgroundColor = "#303440";
            //Detail = nav;

            MasterPage.ListView.SelectedItem = null;
            IsPresented = false;
        }
    }

}
