using LabaTeam1.Model.InternalLogic;
using Model.InternalLogic;
using Model.Players_logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using WpfApp1.Controller;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для RatingWindow.xaml
    /// </summary>

    public class TestData
    {
        public string ModelName { get; set; }
        public string UnitCost { get; set; }
    }
        public partial class RatingWindow : Window
    {
        public RatingWindow(GameWindowPresenter controller)
        {
            PlayersDataGrid = new DataGrid();
            List<Player> players = controller.GetListOfPlayers();
            PlayersDataGrid.ItemsSource = players;
            
            

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
