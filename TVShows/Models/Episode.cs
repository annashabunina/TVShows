using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TVShows.Models
{
   public class Episode
    {
        public bool Watched { get; set; }
        public string ShowName { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("season")]
        public int Season { get; set; }
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("airstamp")]
        public DateTime AirdateDt { get; set; }

        [JsonProperty("runtime")]
        public Double Runtime { get; set; }

        private string summary;
        [JsonProperty("summary")]
        public string Summary
        {
            get { return summary; }
            set
            {
                string str = value?.TrimStart('<', 'p', '>')??"";
                str = str.TrimEnd('<', '/', 'p', '>');
                summary = str;
            }
        }

        [JsonProperty("image")]
        public Image Image { get; set; }
    }

  public  class Image
    {
        [JsonProperty("medium")]
        public string Medium { get; set; }
        [JsonProperty("original")]
        public string Original { get; set; }
    }
}
