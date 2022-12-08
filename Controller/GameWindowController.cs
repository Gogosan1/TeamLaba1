using Model.InternalLogic;
using Modlel.Cards;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApp1.Controller
{
    public class GameWindowController
    {

       public int GetHealth(string type) => game.GetHealth(type);

        public string MakeAMove(List<ICard> cards)
        {
            return game.CompleteRound(cards);
        }

        public int GetGamesRating(string type) => game.GetPointsPerGame(type);
           
        public List<ICard> CardsOfPlayer()
        {
            return game.PlayerCardsInHand();
        }
        private Game game;
    }
}
