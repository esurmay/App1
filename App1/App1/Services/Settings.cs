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

        public static void GetFeeds(string PageName, string Url, ObservableCollection<ItemDetails> Items)
        {
           string responseString = string.Empty;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    responseString = responseContent.ReadAsStringAsync().Result;
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
                                     //Detail = (string)i.Element("description"),
                                     //Link = (string)i.Element("link"),
                                 }).ToList();

                //if (PageName == "Programas")
                //{
                //    int iterador = 0;
                //    query.Select(x =>
                //    {
                //        iterador++;
                //        string original_text = x.Encoded;
                //        string matchString = Regex.Match(original_text, "src=[\"'](.+?)[\"']", RegexOptions.IgnoreCase).Groups[1].Value;
                //        string urlCodeVideo = matchString.Split('/')[matchString.Split('/').Length - 1].ToString();
                //        string tumbnails = "https://img.youtube.com/vi/" + urlCodeVideo + "/" + iterador + ".jpg";

                //        x.Thumbnail = tumbnails;

                //        return x;
                //    }).ToList(); 
                //}

                if (Items.Count <= 1) Items.Clear();
                if (query.Count() > Items.Count)
                {
                    foreach (var item in query)
                        Items.Add(item);
                    {
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
                Items.Add(new ItemDetails { Text = "Ups estas sin conexion a internet." });
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
