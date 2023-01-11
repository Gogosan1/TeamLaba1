using Model.Players_logic;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using WpfApp1.presenter;

namespace WpfApp1.View
{
    public partial class RatingWindow : Window
    {
        public RatingWindow(GameWindowPresenter presenter)
        {
            InitializeComponent();
            PlayersDataGrid.Items.Clear();
            List<Player> players = presenter.GetListOfPlayers();
            DataTable table = new DataTable();
            table.Columns.Add("Имя");
            table.Columns.Add("Рейтинг");
            foreach(Player player in players)
            {
                table.Rows.Add(new object[] {player.NickName, player.GlobalRating.ToString()});
            }

            PlayersDataGrid.ItemsSource = table.DefaultView;
            players.Clear();
        }
    }
}
