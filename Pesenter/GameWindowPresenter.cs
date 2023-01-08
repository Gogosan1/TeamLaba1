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

        public GameWindowPresenter()
        {
            game = new Game();
        }
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

        public bool IsPlayerByNickExist(string name)
        {
            if (game.IsPlayerExist(name))
                return true;
            else
            {
                game.AddPlayer(name);
                return false;
            }
        }
        public List<Player> GetListOfPlayers() => game.GetListOfPlayers();

        private Game game;
    }
}
