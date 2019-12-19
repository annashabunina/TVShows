using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using TVShows.Models;

namespace TVShows
{
    class PersonalArea
    {


   public     List<Show> Shows { get; set; }

      public  List<int> ShowsId { get; set; }

     public   List<int> SeenEpisodesId { get; set; }


        public static Show GetTVShow(string name)
        {

            //return GetQueryResult<Show>($"http://api.tvmaze.com/singlesearch/shows?q={name}");
            Show s= GetQueryResult<Show>($"http://api.tvmaze.com/singlesearch/shows?q={name}");
            s.Episodes = GetQueryResult<List<Episode>>($"http://api.tvmaze.com/shows/{s.Id}/episodes");
            return s;
        }

        private static T GetQueryResult<T>(string query)
        {
            using (var client = new HttpClient())
            {
                string result = client.GetStringAsync(query).Result;  // Blocking call!
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public static List<SearchRating> GetTVShows(string name)
        {
                return GetQueryResult<List<SearchRating>>($"http://api.tvmaze.com/search/shows?q={name}");
        }
       

        public class SearchRating
        {
            [JsonProperty("score")]
            public double Score { get; set; }
            [JsonProperty("show")]
            public Show Show { get; set; }
        }

        private const string ShowsFileName = "shows.json",
           EpisodesFileName = "episodes.json";

        private void SaveList<T>(List<T> data, string fileName)//Code sourse: lecture/seminar samples
        {
            using (var sw = new StreamWriter(fileName))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer()
                    {
                        Formatting = Formatting.Indented
                    };
                    serializer.Serialize(jsonWriter, data);
                }
            }
        }

        public void SaveData()
        {
            SaveList(ShowsId, ShowsFileName);
            SaveList(SeenEpisodesId, EpisodesFileName);
        }

        private List<T> LoadList<T>(string fileName)//Code sourse: lecture/seminar samples
        {
            using (var sr = new StreamReader(fileName))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<List<T>>(jsonReader);
                }
            }
        }

        public void LoadData()
        {
            ShowsId = LoadList<int>(ShowsFileName);
            SeenEpisodesId = LoadList<int>(EpisodesFileName);

            Shows = new List<Show>();
            foreach (var id in ShowsId)
                Shows.Add(GetQueryResult<Show>($"http://api.tvmaze.com/shows/{id}"));

            foreach(var show in Shows)
            {
                show.Episodes = GetQueryResult<List<Episode>>($"http://api.tvmaze.com/shows/{show.Id}/episodes");
                foreach (var episode in show.Episodes)
                    if (SeenEpisodesId.Any(e => e == episode.Id)) episode.Seen = true;
            }
        }
    }
}
