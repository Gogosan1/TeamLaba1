using Model.Cards;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
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
        public GameWindow(GameWindowPresenter controller1)
        {
            ListOfCardsComboBox = new ComboBox();
            controller = controller1;
    
            cardsFromOneMove = new List<ICard>(); 
            cardsFromHand = controller.CardsOfPlayer();
            List<string> cardsDescriptions = new List<string>();

            foreach(var card in cardsFromHand)
            {

                if(typeof(Creature).IsInstanceOfType(card))
                {
                    Creature creature = (Creature)card;
                    string cardDescription = "Creature ";
                    cardDescription += $"Name: {creature.Name} ";
                    cardDescription += $"Health: {creature.Health.ToString()} ";
                    cardDescription += $"Power: {creature.Power.ToString()} ";
                    cardsDescriptions.Add(cardDescription);
                }
                else
                {
                    string cardDescription;
                    if (typeof(HealsPlayerSpell).IsInstanceOfType(card))
                    {
                        cardDescription = "HealsPlayerSpell ";
                    }
                    else
                    {
                        if (typeof(ImprovesPowerSpell).IsInstanceOfType(card))
                        {
                            cardDescription = "ImprovesPowerSpell ";
                        }
                        else
                            cardDescription = "ImprovesProtectionSpell ";
                    }
                    cardDescription += $"Name: {card.Name} ";
                    cardDescription += $"Power: {card.Power.ToString()} ";
                    cardsDescriptions.Add(cardDescription);
                }

            }
            ListOfCardsComboBox.Items.Add(cardsDescriptions);
            InitializeComponent();
        }


        private void MakeAMoveButton_Click(object sender, RoutedEventArgs e)
        {
            string[] strings = ListOfCardsComboBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (ListOfCardsComboBox.Text.Contains("Creature") && cardsFromOneMove.Count == 0)
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
                    GameLogsTextBlock.Text = controller.MakeAMove(cards);
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
                        GameLogsTextBlock.Text = controller.MakeAMove(cards);

                    }
                }
              
            
            PlayersHealthLabel.Content = controller.GetHealth("Player");
            BotHealthLabel.Content = controller.GetHealth("Bot");
          //  Пергамес притягивать а не гаме ратинг
            PlayersGamesRatingLabel.Content = controller.GetGamesRating("Player");
            BotsGamesRatingLabel.Content = controller.GetGamesRating("Bot");
        }


        private bool TakeBoost;
        private List<ICard> cardsFromOneMove;
        private List<ICard> cardsFromHand;
        private GameWindowPresenter controller;
    }
}
