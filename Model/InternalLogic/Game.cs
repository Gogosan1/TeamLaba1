using Model.Players_logic;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Web;

namespace Model.InternalLogic
{
    public class Game
    {
        private DataManager dm;
        private List<IPlayer> players;
        private int CounterOfRounds;

        public Game()
        {
            dm = new DataManager();
            players = new List<IPlayer>();
            CounterOfRounds = 0;
        }

        public void AddPlayer (string name)
        {
            List<ICard> cardsForOneGame = new List<ICard>();

            if (!IsPlayerExist(name))
            {
              //  cardsForOneGame = DealCards();
                var player = new Player(name, cardsForOneGame, 0);
                players.Add(player);
                dm.SavePlayerData(player);
            }
            else
            {
                foreach (Player p in dm.AllPlayers)
                {
                    if (p.NickName == name)
                    {
                        cardsForOneGame = DealCards();
                        players.Add(p);
                    }
                }
            }
        }
        public bool IsPlayerExist(string inputNickName)
        {
            var bot = new Bot(dm.AllCards);
            players.Add(bot);

            bool IsOldPlayer = false;
            foreach (Player p in dm.AllPlayers)
            {
                if (p.NickName == inputNickName)
                {
                    IsOldPlayer = true;
                }
            }
            return IsOldPlayer;
        }

        private List<ICard> DealCards ()
        {
            List<ICard> cardsForOneGame = new List<ICard>();
            for (int i = 0; i < 6; i++)
            {
                Random rand = new Random();
                int index = rand.Next(dm.AllCards.Count);
                cardsForOneGame.Add(dm.AllCards[index]);
            }
            return cardsForOneGame;
        }

        public string CompleteRound(List<ICard> cards) // срабатывает при нажатии на кнопкку
        {
            string MessageLog = "";
            // либо сделать через возврат логов либо через подписку
            List<ICard> playerCards = cards;
            Bot bot = new Bot();
            foreach ( var player in players)
            {
                if (typeof(Bot).IsInstanceOfType(player))
                    {
                     bot = (Bot)player;
                    }
            }
            List<ICard> botCards = bot.PutCardFromHandOnTheTable();

            // тут логика взамодействия карт
            // 

            // отрабатывает логика с заклинаниями
            if (playerCards.Count == 2)
             //   ICard NewCardCreatureP = UseSpell(playerCards);

            if (botCards.Count == 2)
               // ICard NewCardCreatureB = UseSpell(botCards);

            // отрабатывает логика с начислением очков и отниманием здоровья



            CounterOfRounds++;

            if (CounterOfRounds == 12 /* или кто-то умер...*/)
                throw new Exception("Игра закончена");

            return null; // возвращаем большое сообщение что произошло
        }

        public List<ICard> PlayerCardsInHand()
        {
            foreach (var player in players)
                if (typeof(Player).IsInstanceOfType(player))
                    return player.CardsInHands;
            return null;
        }

        public ObservableCollection<Player> GetListOfPlayers()
        {
            return dm.AllPlayers;
        }

        public int GetHealth(string type)
        {
            foreach ( var player in players)
            {
                if (typeof(Player).IsInstanceOfType(player) && (type == "Player"))
                    return player.GetHealth();
                if (typeof(Bot).IsInstanceOfType(player) && (type == "Bot"))
                    return player.GetHealth();
            }
            return 0;
        }

        public int GetPointsPerGame(string type)
        {
            foreach (var player in players)
            {
                if (typeof(Player).IsInstanceOfType(player) && (type == "Player"))
                    return player.GetPointsPerGame();
                if (typeof(Bot).IsInstanceOfType(player) && (type == "Bot"))
                    return player.GetPointsPerGame();
            }
            return 0;
        }
    }
}
