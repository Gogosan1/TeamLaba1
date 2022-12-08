using Model.Players_logic;
using Modlel.Cards;
using System;
using System.Collections.Generic;


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
            if (!IsPlayerExist(name))
            {
             var player = new Player(name, dm.AllCards, 0);
                players.Add(player);
                dm.SavePlayerData(player);
            }
            else
            {
                foreach (Player p in dm.AllPlayers)
                {
                    if (p.NickName == name)
                    {
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


        public string CompleteRound() // срабатывает при нажатии на кнопкку
        {
            List<ICard> playerCards = player.PutCardFromHandOnTheTable();
            List<ICard> botCards = bot.PutCardFromHandOnTheTable();

            // тут логика взамодействия карт
            // 

            // отрабатывает логика с заклинаниями
            if (playerCards.Count == 2) 
                ICard NewCard = UseSpell(playerCards);

            if (botCards.Count == 2)
                ICard NewCard = UseSpell(botCards);

            // отрабатывает логика с начислением очков и отниманием здоровья



            CounterOfRounds++;
        }

    }
}
