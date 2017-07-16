using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LevantateChevere.Services
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

        public static async Task GetFeedsAsync(string PageName, string Url, ObservableCollection<ItemDetails> Items)
        {

            if (Items.Count <= 1) Items.Clear();

            if (CrossConnectivity.Current.IsConnected)
            {
                string responseString = string.Empty;

                using (var client = new HttpClient())
                {
                    using (var r = await client.GetAsync(new Uri(Url)))
                    {
                        responseString = await r.Content.ReadAsStringAsync();
                    }
                    
                }
                responseString = responseString.TrimStart();
                if (!string.IsNullOrEmpty(responseString))
                {
                    List<ItemDetails> query = XDocument.Parse(responseString)
                                     .Descendants("item")
                                     .Select(i => new ItemDetails
                                     {
                                         Text = (string)i.Element("title"),
                                         Detail = Convert.ToDateTime((string)i.Element("pubDate")).ToString("dd/MM/yyyy"),
                                         Encoded = (string)i.Element("{http://purl.org/rss/1.0/modules/content/}encoded"), //<-- ***
                                     }).ToList();

                    if (query.Count() > Items.Count)
                    {
                        foreach (var item in query)
                            Items.Add(item);

                        switch (PageName)
                        {
                            case "Noticias":
                                ListNoticias = Items;
                                break;
                            case "Patrocinantes":
                                ListPatrocinantes = Items;
                                break;
                            case "Programas":
                                ListProgramas = Items;

                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                Items.Add(new ItemDetails { ImageUrl = "NoConnected.png", Thumbnail = "NoConnected.png", Text = "No Internet Connection" });
            }
        }
    }


    public class ItemDetails
    {
        public string Text { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public string Encoded { get; set; }
        public string Thumbnail { get; set; }

        public override string ToString() => Text;
    }
}
