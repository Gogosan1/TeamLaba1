using Model.Cards;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        // полностью переделать тут вобще все не верно
        private void MakeAMoveButton_Click(object sender, RoutedEventArgs e)
        {
            // самым первым действием нужно связать датагрид с данными из игрока который находиться в классе Game
            // подписаться на событие
            List<ICard> cards = new List<ICard>();
            if (radiobatton1.Checked)
            {
                ICard cardFromMove = DataGrid; // присвоить значение выбранного 
                if (typeof(Creature).IsInstanceOfType(cardFromMove))
                {
                    cards.Add(cardFromMove);
                    // сделать проверку есть ли вобще заклинания или нет
                    MessageBox.Show(" Хотите добваить буст к вашему существу?"); // добваить 2 кнопки да/нет
                    if (нет)
                    {
                        GameLogsTextBlock.Text += controller.MakeAMove(cards);
                    }
                    else
                    {
                        if (radiobatton1.Checked)
                        {
                            ICard cardFromMove = DataGrid; // присвоить значение выбранного 
                            if (!typeof(Creature).IsInstanceOfType(cardFromMove))
                            {
                                cards.Add(cardFromMove);
                                GameLogsTextBlock.Text += controller.MakeAMove(cards);
                            }
                            else
                            {
                                MessageBox.Show("Нужно выбрать заклинание");
                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Выберите существо");
                }
            }
            PlayersHealthLabel.Content = controller.GetHealth("Player");
            BotHealthLabel.Content = controller.GetHealth("Bot");
            PlayersGamesRatingLabel.Content = controller.GetGamesRating("Player");
            BotsGamesRatingLabel.Content = controller.GetGamesRating("Bot");
        }

        private GameWindowController controller;
    }
}
