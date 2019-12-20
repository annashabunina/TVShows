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

        }

        private void Update()
        {
            ShowsListBox.Items.Clear();
            ToWatchListBox.Items.Clear();
            WatchedListBox.Items.Clear();
            var shows = personalArea.GetCurrentShows();
            foreach (Show show in shows)
                ShowsListBox.Items.Add(show);
            var WatchedEpisodes = personalArea.GetEpisodes(true);
            foreach (Episode e in WatchedEpisodes)
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

        private void TowatchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Episode episode = (Episode)WatchedListBox.SelectedItem;
                personalArea.ChangeEpisodeState(episode.Id, false);
            }
            catch { }
        }

        private void WatchedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Episode episode = (Episode)ToWatchListBox.SelectedItem;
                if (episode.AirdateDt > DateTime.Now) MessageBox.Show("Episode is not realeased");
                else
                personalArea.ChangeEpisodeState(episode.Id, true);
            }
            catch { }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                personalArea.DeleteShow(((Show)ShowsListBox.SelectedItem).Id);
            }
            catch { }
        }

        private void ShowsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowWindow showWindow = new ShowWindow(personalArea, (Show)ShowsListBox.SelectedItem);
            showWindow.ShowDialog();
        }
    }
}
