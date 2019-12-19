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
        // INSERT YOUR OPENWEATHERMAP KEY BEFORE RUNNING THE PROGRAM
        const string AppId = "";

        static string BuildUrl(string baseUrl, IDictionary<string, string> parameters)
        {
            var sb = new StringBuilder(baseUrl);
            if (parameters?.Count > 0)
            {
                sb.Append('?');
                foreach (var p in parameters)
                {
                    sb.Append(p.Key);
                    sb.Append("=");
                    sb.Append(WebUtility.UrlEncode(p.Value));  // This is the important part - percent-encoding
                    sb.Append('&');
                }
                sb.Remove(sb.Length - 1, 1);    // Remove the last '&'
            }
            return sb.ToString();
        }

        public static string MakeQuery(string name)
        {
            // Simple option
            //return $"http://api.tvmaze.com/singlesearch/shows?q=girls";
            return $"http://api.tvmaze.com/singlesearch/shows?q={name}";

            // A better option:
            //return BuildUrl("http://api.tvmaze.com/singlesearch/shows?q={name}", new Dictionary<string, string>
            //{
            //    {"name", "girls" },
            //    //{"id", "metric" },
            //    //{"appid", AppId }
            //});
        }

        public static string MakeQuery(int id)
        {

            return $"http://api.tvmaze.com/singlesearch/shows?q={id}";
        }

        public static TVShow GetTVShow(string city)
        {
            using (var client = new HttpClient())
            {
                string result = client.GetStringAsync(MakeQuery(city)).Result;  // Blocking call!
                TVShow s= JsonConvert.DeserializeObject<TVShow>(result);
                string r = client.GetStringAsync($"http://api.tvmaze.com/shows/{s.Id}/episodes").Result;
                s.Episodes= JsonConvert.DeserializeObject<List<Episode>>(r);
                return s;
            }
        }

        public static List<SearchRating> GetTVShows(string name)
        {
            using (var client = new HttpClient())
            {
                string result = client.GetStringAsync($"http://api.tvmaze.com/search/shows?q={name}").Result;
                return JsonConvert.DeserializeObject<List<SearchRating>>(result);
            }
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
