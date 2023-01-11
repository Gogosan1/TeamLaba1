using LabaTeam1.Model.Cards;
using Model.Cards;
using Model.InternalLogic;
using Model.Players_logic;
using Modlel.Cards;
using System.Collections.Generic;

namespace WpfApp1.presenter
{
    public class GameWindowPresenter
    {
        public GameWindowPresenter(string nameOfPlayer)
        {
            NameOfPlayer = nameOfPlayer;
            game = new Game(nameOfPlayer);
        }

        public List<string> DrawTheCreaturesDeck()
        {
            List<ICard> cardsFromHand = GetListOfPlayersCardsInGame();
           
            List<string> cardsDescriptions = new List<string>();

            foreach (var card in cardsFromHand)
            {
           
                if (typeof(Creature).IsInstanceOfType(card))
        {
                    Creature creature = (Creature)card;
                    string cardDescription = "Существо\n";
                    cardDescription += $"Имя: {creature.Name}\n";
                    cardDescription += $"Здоровье: {creature.Health.ToString()}\n";
                    cardDescription += $"Сила: {creature.Power.ToString()}";
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
                string cardDescription = "";
                if (typeof(HealsPlayerSpell).IsInstanceOfType(card))
                {
                    cardDescription = "Улучшающее здоровье игрока заклинание:\n";
                }
                else
                {
                    if (typeof(ImprovesPowerSpell).IsInstanceOfType(card))
                    {
                        cardDescription = " Улучшающее силу заклинание:\n";
                    }
                    else
                    {
                        if (typeof(ImprovesProtectionSpell).IsInstanceOfType(card))
                            cardDescription = "Улучшающее здоровье существа заклинание:\n";
                    }
                }
                    if (!typeof(Creature).IsInstanceOfType(card))
                    {
                        cardDescription += $"Имя: {card.Name}\n";
                        cardDescription += $"Сила: {card.Power.ToString()}";
                        cardsDescriptions.Add(cardDescription);
                    }
                
            }
            return cardsDescriptions;
        }
        

        public string MakeAMove(List<ICard> cards) => game.CompleteRound(cards);
        public bool IsGameOver() => game.IsGameOver;
        public string GameOverMessage() => game.FinishGameInfo();

        public void PressEndGameButton()
        {
            game.ReduceGlobalRating();
        }
        public int GetHealth(string type) => game.GetHealth(type);
        public int GetGamesRatingPerGame(string type) => game.GetPointsPerGame(type);
        public List<Player> GetListOfPlayers() => game.GetListOfPlayers();
        public List<ICard> GetListOfPlayersCardsInGame() => game.GetListOfPlayersCardsInGame();
        public int GetCounterOfRounds() => game.CounterOfRounds;
            
        private Game game;
        public string NameOfPlayer { get; init; }
    }
}
