using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TVShows.Models
{
   public class Show
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("runtime")]
        public double? Runtime { get; set; }

        [JsonProperty("premiered")]
        public DateTime? PremieredDt { get; set; }

        public List<Episode> Episodes { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        private string summary;
        [JsonProperty("summary")]
        public string Summary
        {
            get { return summary; }
            set { string str = value?.TrimStart('<','p','>')??"";
                str = str.TrimEnd('<', '/', 'p', '>');
                summary = str;
            }
        }




    }
}
