using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TVShows.Models
{
    class TVShow
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("runtime")]
        public double Runtime { get; set; }

        [JsonProperty("premiered")]
        public DateTime PremieredDt { get; set; }

        public List<Episode> Episodes { get; set; }

        //[JsonProperty("image")]
        //public List<Dictionary<string,string>> Image { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        /*[{"id":139,"url":"http://www.tvmaze.com/shows/139/girls","name":"Girls","type":"Scripted","language":"English","genres":["Drama","Romance"],"status":"Ended","runtime":30,"premiered":"2012-04-15","officialSite":"http://www.hbo.com/girls","schedule":{"time":"22:00","days":["Sunday"]},"rating":{"average":6.8},"weight":89,"network":{"id":8,"name":"HBO","country":{"name":"United States","code":"US","timezone":"America/New_York"}},"webChannel":null,"externals":{"tvrage":30124,"thetvdb":220411,"imdb":"tt1723816"},"image":{"medium":"http://static.tvmaze.com/uploads/images/medium_portrait/31/78286.jpg","original":"http://static.tvmaze.com/uploads/images/original_untouched/31/78286.jpg"},"summary":"<p>This Emmy winning series is a comic look at the assorted humiliations and rare triumphs of a group of girls in their 20s.</p>","updated":1573153623,"_links":{"self":{"href":"http://api.tvmaze.com/shows/139"},"previousepisode":{"href":"http://api.tvmaze.com/episodes/1079686"}}}}*/
    }
}
