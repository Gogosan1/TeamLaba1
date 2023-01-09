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
            cardsFromOneMove = new List<ICard>();
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

        // очень хрупкая фигня
        private void MakeAMoveButton_Click(object sender, RoutedEventArgs e)
        {
            string[] strings = ListOfCreaturesComboBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // сделать проверку на пустоту листбоксов

            if (ListOfCreaturesComboBox.Text.Contains("Creature") && cardsFromOneMove.Count == 0)
            {
                string cardName = "";
                for (int i = 0; i < strings.Length - 1; i++)
                {
                    if (strings[i] == "Name:")
                        cardName = strings[i + 1];

                }
                foreach (var card in presenter.GetListOfPlayersCardsInGame())
                    if (card.Name == cardName)
                        cardsFromOneMove.Add(card);


                // сделать проверку есть ли вобще заклинания или нет
                var dialogResult = MessageBox.Show(" Хотите добваить буст к вашему существу?", "Choise", MessageBoxButton.YesNo); // добваить 2 кнопки да/нет
                if (dialogResult == MessageBoxResult.Yes)
                {
                    TakeBoost = true;
                }
                else
                {
                    TakeBoost = false;
                    List<ICard> cards = cardsFromOneMove;
                    cardsFromOneMove.Clear();
                    GameLogsTextBlock.Text = presenter.MakeAMove(cards);
                   // Вызывать метод игра закончена
                }
            }
                
                if (TakeBoost == true)
                {
                    if (!ListOfCardsComboBox.Text.Contains("Creature"))
                    {
                        string cardName = "";
                        for (int i = 0; i < strings.Length - 1; i++)
                        {
                            if (strings[i] == "Name:")
                                cardName = strings[i + 1];

                        }

                        foreach (var card in cardsFromHand)
                            if (card.Name == cardName)
                                cardsFromOneMove.Add(card);

                        TakeBoost = false;
                        List<ICard> cards = cardsFromOneMove;
                        cardsFromOneMove.Clear();
                        GameLogsTextBlock.Text = presenter.MakeAMove(cards);

                    }
                }
              
            
            DrawHealthAndGamePoints();
            DrawDecks();
        }


        private bool TakeBoost;
        private List<ICard> cardsFromOneMove;
        private GameWindowPresenter presenter;
    }
}
