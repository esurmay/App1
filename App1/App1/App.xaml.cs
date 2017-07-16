using LevantateChevere.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LevantateChevere
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Chevere();
 
            //SetMainPage();
        }

       
    }
}
