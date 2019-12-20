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
using static TVShows.PersonalArea;

namespace TVShows
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        PersonalArea personalArea;

        public Search(PersonalArea personalArea)
        {
            InitializeComponent();
            this.personalArea = personalArea;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ListBoxResult.Items.Clear();
            string query = SearchBox.Text;

            List<SearchRating> result=personalArea.SearchTVShows(query);
            foreach (var t in result)
                ListBoxResult.Items.Add(t.Show);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Show show=(Show)ListBoxResult.SelectedItem;
                personalArea.AddShow(show.Id);
            }
            catch
            {
                MessageBox.Show("Choose");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
