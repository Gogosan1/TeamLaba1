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
            PlayersHealthLabel.Content = presenter.GetHealth("Player");
            BotHealthLabel.Content = presenter.GetHealth("Bot");
            //  Пергамес притягивать а не гаме ратинг
            PlayersGamesRatingLabel.Content = presenter.GetGamesRating("Player");
            BotsGamesRatingLabel.Content = presenter.GetGamesRating("Bot");
        }


        private void MakeAMoveButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (ListOfCreaturesComboBox.Text == null)
                MessageBox.Show("Выберите существо!");
            else 
            {
                string[] CreaturesStrings = ListOfCreaturesComboBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<ICard> cardsFromOneMove = new List<ICard>();
                string cardName = "";
                for (int i = 0; i < CreaturesStrings.Length - 1; i++)
                {
                    if (CreaturesStrings[i] == "Name:")
                        cardName = CreaturesStrings[i + 1];

                }
                foreach (var card in presenter.GetListOfPlayersCardsInGame())
                    if (card.Name == cardName)
                        cardsFromOneMove.Add(card);

                if (ListOfSpellsComboBox.Text != null)
                {
                    string[] SpellsStrings = ListOfSpellsComboBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string cardName1 = "";
                    for (int i = 0; i < SpellsStrings.Length - 1; i++)
                    {
                        if (SpellsStrings[i] == "Name:")
                            cardName1 = SpellsStrings[i + 1];

                    }
                    foreach (var card in presenter.GetListOfPlayersCardsInGame())
                        if (card.Name == cardName)
                            cardsFromOneMove.Add(card);
                }
                  //  GameLogsTextBlock.Text += presenter.MakeAMove(cardsFromOneMove);
                // Вызывать метод игра закончена
              /*  if (presenter.IsGameOver())
                {
                    this.Close();
                    MessageBox.Show(presenter.GameOverMessage());
                    
                }*/
                }
            
            DrawHealthAndGamePoints();
            DrawDecks();
        }

        private GameWindowPresenter presenter;
    }
}
