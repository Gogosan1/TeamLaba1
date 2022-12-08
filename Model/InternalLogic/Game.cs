using Model.Players_logic;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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
            players = new List<IPlayer>();
            CounterOfRounds = 0;
        }

        public void AddPlayer (string name)
        {
            List<ICard> cardsForOneGame = new List<ICard>();

            if (!IsPlayerExist(name))
            {
                cardsForOneGame = DealCards();
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
            List<ICard> botCards = new List<ICard>();
            foreach ( var player in players)
            {
                if (typeof(Bot).IsInstanceOfType(player))
                    {

                    }
            }
            List<ICard> botCards = bot.PutCardFromHandOnTheTable();

            // тут логика взамодействия карт
            // 

            // отрабатывает логика с заклинаниями
            if (playerCards.Count == 2)
                ICard NewCardCreatureP = UseSpell(playerCards);

            if (botCards.Count == 2)
                ICard NewCardCreatureB = UseSpell(botCards);

            // отрабатывает логика с начислением очков и отниманием здоровья



            CounterOfRounds++;

            if (CounterOfRounds == 12 /* или кто-то умер...*/)
                throw new Exception("Игра закончена");

            return // возвращаем большое сообщение что произошло
        }

    }
}
