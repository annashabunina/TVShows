using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TVShows.Models;

namespace TVShows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PersonalArea personalArea;
        public MainWindow()
        {
            InitializeComponent();
            personalArea = new PersonalArea();
            Update();
            personalArea.ChangedShow += Update;
            //var s = PersonalArea.GetTVShow("girls");
            ////var ss = PersonalArea.GetTVShows("game of thrones");
            //PersonalArea p = new PersonalArea();
            ////p.Shows = new List<Show>();
            ////p.Shows.Add(s);
            ////p.ShowsId = new List<int>();
            ////p.ShowsId.Add(s.Id);
            ////p.SeenEpisodesId = new List<int>();
            ////p.SeenEpisodesId.Add(s.Episodes[3].Id);
            ////p.SaveData();
            //p.LoadData();
            //var s=p.Shows;
        }

        private void Update()
        {
            ShowsListBox.Items.Clear();
            var shows = personalArea.GetCurrentShows();
            foreach (Show show in shows)
                ShowsListBox.Items.Add(show);
            var WatchedEpisodes = personalArea.GetEpisodes(true);
            foreach(Episode e in WatchedEpisodes)
            WatchedListBox.Items.Add(e);
            var NotWatcedEpisodes = personalArea.GetEpisodes(false);
            foreach (Episode e in NotWatcedEpisodes)
                ToWatchListBox.Items.Add(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Search searchWindow = new Search(personalArea);
            searchWindow.ShowDialog();
        }
    }
}
