using Model.Cards;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Controller;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow(GameWindowPresenter pres)
        {
            presenter = pres;
            InitializeComponent();
            DrawDecks();
            DrawHealthAndGamePoints();
        }

        private void DrawDecks()
        {
            ListOfCreaturesComboBox.Items.Clear();
            ListOfSpellsComboBox.Items.Clear();
            List<string> cardsDescriptions = new List<string>();

            cardsDescriptions = presenter.DrawTheCreaturesDeck();
            foreach (var cardsDescription in cardsDescriptions)
                ListOfCreaturesComboBox.Items.Add(cardsDescription);

            cardsDescriptions = presenter.DrawTheSpellsDeck();
            foreach (var cardsDescription in cardsDescriptions)
                ListOfSpellsComboBox.Items.Add(cardsDescription);

        }

        private void DrawHealthAndGamePoints()
        {
            PlayersHealthLabel.Content = "Ваше здоровье: " + presenter.GetHealth("Player");
            BotHealthLabel.Content = "Здоровье соперника: " +presenter.GetHealth("Bot");
            
            PlayersGamesRatingLabel.Content = "Ваши очки: " + presenter.GetGamesRatingPerGame("Player");
            BotsGamesRatingLabel.Content = "Очки соперника: " + presenter.GetGamesRatingPerGame("Bot");
        }


        private void MakeAMoveButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (ListOfCreaturesComboBox.Text == "")
                MessageBox.Show("Выберите существо!");
            else 
            {
                string[] CreaturesStrings = ListOfCreaturesComboBox.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                List<ICard> cardsFromOneMove = new List<ICard>();
                string CreatureCardName = "";
                for (int i = 0; i < CreaturesStrings.Length - 1; i++)
                {
                    if (CreaturesStrings[i].Contains(Constants.CARD_NAME))
                        CreatureCardName = CreaturesStrings[i].Substring(5);

                }
                foreach (var card in presenter.GetListOfPlayersCardsInGame())
                    if (card.Name == CreatureCardName)
                        cardsFromOneMove.Add(card);

                if (ListOfSpellsComboBox.Text != "")
                {
                    string[] SpellsStrings = ListOfSpellsComboBox.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    string SpellCardName = "";

                    for (int i = 0; i < SpellsStrings.Length - 1; i++)
                    {
                        if (SpellsStrings[i].Contains(Constants.CARD_NAME))
                            SpellCardName = SpellsStrings[i].Substring(5);

                    }

                    foreach (var card in presenter.GetListOfPlayersCardsInGame())
                        if (card.Name == SpellCardName)
                            cardsFromOneMove.Add(card);
                }

                  GameLogsTextBlock.Text = presenter.MakeAMove(cardsFromOneMove);
                // Вызывать метод игра закончена
                if (presenter.IsGameOver())
                {
                    //this.Close();
                    MessageBox.Show(presenter.GameOverMessage());
                    
                }
                ListOfCreaturesComboBox.SelectedIndex = -1;
                ListOfSpellsComboBox.SelectedIndex = -1;
                DrawHealthAndGamePoints();
                DrawDecks();
            }
            
        }

        private GameWindowPresenter presenter;
    }
}
