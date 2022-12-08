using Model.InternalLogic;
using Modlel.Cards;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApp1.Controller
{
    public class GameWindowController
    {
        public GameWindowController()
        {
            game = new Game();
        }

        public int GetHealth() =>

        public string MakeAMove(List<ICard> cards)
        {
            return game.CompleteRound(cards);
        }

        public int GetGamesRating() =>
           

        private Game game;
    }
}
