using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{
    public static class Settings
    {
    
        public static System.Collections.ObjectModel.ObservableCollection<ItemDetails> ListNoticias
        {
            get;
            set;
        }
        public static System.Collections.ObjectModel.ObservableCollection<ItemDetails> ListProgramas
        {
            get;
            set;
        }

        public static System.Collections.ObjectModel.ObservableCollection<ItemDetails> ListPatrocinantes
        {
            get;
            set;
        }
    }

    public class ItemDetails
    {
        public string Text { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }

        

        public override string ToString() => Text;
    }
}
