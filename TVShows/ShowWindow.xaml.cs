using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TVShows.Models;

namespace TVShows
{
    /// <summary>
    /// Логика взаимодействия для ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        PersonalArea personalArea;
        Show show;
        public ShowWindow(PersonalArea personalArea, Show show)
        {

            InitializeComponent();
            personalArea.ChangedShow += Update;
            this.show = show;
            this.personalArea = personalArea;
            Update();
        }
        private void Update()
        {
            EpisodesListBox.Items.Clear();
            if (show?.Episodes != null)
            foreach (Episode e in show.Episodes)
                EpisodesListBox.Items.Add(e);
        }

        private void EpisodesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Episode episode = (Episode)EpisodesListBox.SelectedItem;
                if (episode.AirdateDt > DateTime.Now) MessageBox.Show("Episode is not realeased");
                else
                    personalArea.ChangeEpisodeState(episode.Id, !episode.Watched);
            }
            catch { }
        }
    }
}
