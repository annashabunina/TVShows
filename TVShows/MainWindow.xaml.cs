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
        public MainWindow()
        {
            InitializeComponent();

            ////PersonalArea personalArea = new PersonalArea();
            ////var s = PersonalArea.GetTVShow("girls");
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
    }
}
