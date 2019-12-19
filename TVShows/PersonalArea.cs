using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using TVShows.Models;

namespace TVShows
{
    class PersonalArea
    {


        List<TVShow> Shows { get; set; }

        List<int> ShowsId { get; set; }

        List<int> SeenEpisodesId { get; set; }


        public static TVShow GetTVShow(string name)
        {

            return GetQueryResult<TVShow>($"http://api.tvmaze.com/singlesearch/shows?q={name}");
            
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
            public TVShow Show { get; set; }
        }
    }
}
