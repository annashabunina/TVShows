﻿using Newtonsoft.Json;
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
    public class PersonalArea
    {
      public  PersonalArea()
        {
            try
            {
                LoadData();
            }
            catch
            {
                Shows = new List<Show>();
                ShowsId = new List<int>();
                WhatchedEpisodesId = new List<int>();
            }
        }

        List<Show> Shows { get; set; }
        List<int> ShowsId { get; set; }
        List<int> WhatchedEpisodesId { get; set; }

        public Action ChangedEpisodeState;
        public Action ChangedShow;


        public List<Episode> GetEpisodes(bool watched)
        {
            List<Episode> episodes = new List<Episode>();
            foreach (Show show in Shows)
                episodes.AddRange(show.Episodes.FindAll(e => e.Watched == watched));
            return episodes;
        }

        public void ChangeEpisodeState(int id, bool watched)
        {
            Episode episode=null;
            foreach (Show show in Shows)
                episode = show.Episodes.FirstOrDefault(e => e.Id == id);
            episode.Watched = watched;
            ChangedEpisodeState?.Invoke();
        }
        //public List<Episode> MyProperty { get; set; }


        //public Show GetTVShow(string name)
        //{
        //    //return GetQueryResult<Show>($"http://api.tvmaze.com/singlesearch/shows?q={name}");
        //    Show s= GetQueryResult<Show>($"http://api.tvmaze.com/singlesearch/shows?q={name}");
        //    s.Episodes = GetQueryResult<List<Episode>>($"http://api.tvmaze.com/shows/{s.Id}/episodes");
        //    return s;
        //}

        private T GetQueryResult<T>(string query)
        {
            using (var client = new HttpClient())
            {
                string result = client.GetStringAsync(query).Result;  // Blocking call!
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public  List<SearchRating> SearchTVShows(string name)
        {
                return GetQueryResult<List<SearchRating>>($"http://api.tvmaze.com/search/shows?q={name}");
        }

        public bool AddShow(int id)
        {
            if (ShowsId.Any(s => s == id)) return false;
            ShowsId.Add(id);
            var show=GetQueryResult<Show>($"http://api.tvmaze.com/shows/{id}");
            show.Episodes = GetQueryResult<List<Episode>>($"http://api.tvmaze.com/shows/{id}/episodes");
            Shows.Add(show);
            SaveData();
            ChangedShow?.Invoke();
            return true;
        }

        public bool DeleteShow(int id)
        {
            if (!ShowsId.Any(s => s == id)) return false;
            Show show = Shows.First(s => s.Id == id);
            ShowsId.Remove(id);
            Shows.Remove(show);
            SaveData();
            ChangedShow?.Invoke();
            return true;
        }

        #region save&load

        private const string ShowsFileName = "shows.json",
           EpisodesFileName = "episodes.json";

        private void SaveList<T>(List<T> data, string fileName)//Code source: lecture/seminar samples
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
        private void SaveData()
        {
            SaveList(ShowsId, ShowsFileName);
            SaveList(WhatchedEpisodesId, EpisodesFileName);
        }

        private List<T> LoadList<T>(string fileName)//Code source: lecture/seminar samples
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

        private void LoadData()
        {
            ShowsId = LoadList<int>(ShowsFileName);
            WhatchedEpisodesId = LoadList<int>(EpisodesFileName);

            Shows = new List<Show>();
            foreach (var id in ShowsId)
                Shows.Add(GetQueryResult<Show>($"http://api.tvmaze.com/shows/{id}"));

            foreach(var show in Shows)
            {
                show.Episodes = GetQueryResult<List<Episode>>($"http://api.tvmaze.com/shows/{show.Id}/episodes");
                foreach (var episode in show.Episodes)
                    if (WhatchedEpisodesId.Any(e => e == episode.Id)) episode.Watched = true;
            }
        }
        #endregion save&load

        public class SearchRating
        {
            [JsonProperty("score")]
            public double Score { get; set; }
            [JsonProperty("show")]
            public Show Show { get; set; }
        }
    }

}
