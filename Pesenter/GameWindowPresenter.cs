using Model.Cards;
using Model.InternalLogic;
using Model.Players_logic;
using Modlel.Cards;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApp1.Controller
{
    public class GameWindowPresenter
    {

        public GameWindowPresenter(string nameOfPlayer)
        {
            game = new Game(nameOfPlayer);
        }
       
        public string MakeAMove(List<ICard> cards)
        {
            return null; // game.CompleteRound(cards);
        }

        public List<string> DrawTheCreaturesDeck ()
        {
            List<ICard> cardsFromHand = GetListOfPlayersCardsInGame();
           
            List<string> cardsDescriptions = new List<string>();

            foreach (var card in cardsFromHand)
            {

                if (typeof(Creature).IsInstanceOfType(card))
                {
                    Creature creature = (Creature)card;
                    string cardDescription = "Creature \n";
                    cardDescription += $"Name: {creature.Name} \n";
                    cardDescription += $"Health: {creature.Health.ToString()} \n";
                    cardDescription += $"Power: {creature.Power.ToString()} ";
                    cardsDescriptions.Add(cardDescription);
                }
               
            }
            return cardsDescriptions;
        }
        public List<string> DrawTheSpellsDeck()
        {
            List<ICard> cardsFromHand = GetListOfPlayersCardsInGame();

            List<string> cardsDescriptions = new List<string>();

            foreach (var card in cardsFromHand)
            {
                string cardDescription;
                if (typeof(HealsPlayerSpell).IsInstanceOfType(card))
                {
                    cardDescription = "HealsPlayerSpell \n";
                }
                else
                {
                    if (typeof(ImprovesPowerSpell).IsInstanceOfType(card))
                    {
                        cardDescription = "ImprovesPowerSpell \n";
                    }
                    else
                        cardDescription = "ImprovesProtectionSpell \n";
                }
                cardDescription += $"Name: {card.Name} \n";
                cardDescription += $"Power: {card.Power.ToString()} ";
                cardsDescriptions.Add(cardDescription);
            }

            return cardsDescriptions;
        }

        public int GetHealth(string type) => game.GetHealth(type);
        public int GetGamesRating(string type) => game.GetPointsPerGame(type);

        public bool IsPlayerByNickExist(string name) => game.IsPlayerExist(name);
        public List<Player> GetListOfPlayers() => game.GetListOfPlayers();
        public List<ICard> GetListOfPlayersCardsInGame() => game.GetListOfPlayersCardsInGame();
        private Game game;
    }
}
