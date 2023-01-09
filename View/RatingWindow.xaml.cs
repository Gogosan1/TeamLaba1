using Model.Players_logic;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using WpfApp1.Controller;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для RatingWindow.xaml
    /// </summary>

    public partial class RatingWindow : Window
    {
        public RatingWindow(GameWindowPresenter controller)
        {
            InitializeComponent();
            //PlayersDataGrid = new DataGrid();
            List<Player> players = controller.GetListOfPlayers();
            DataTable table = new DataTable();
            table.Columns.Add("Имя");
            table.Columns.Add("Рейтинг");
            foreach(Player player in players)
            {
                table.Rows.Add(new object[] {player.NickName, player.GlobalRating.ToString()});
            }

            PlayersDataGrid.ItemsSource = table.DefaultView;
            
        }
    }
}
