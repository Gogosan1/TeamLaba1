using Model.Cards;
using Model.Players_logic;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Web;
using LabaTeam1.Model.InternalLogic;

namespace Model.InternalLogic
{
    public class Game
    {
        private DataManager dm = new DataManager();
        private List<IPlayer> players;
        private int CounterOfRounds;
        private bool PlayerWon = false;
        private string gameStatus = "";
        IAnylyzingMove anylyzingMove;
        IFinishGame finishGame;
        IDescribeCard describeCard;

        public Game()
        {
            players = new List<IPlayer>();
            CounterOfRounds = 0;
        }

        public void AddPlayer(string name)
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

        private List<ICard> DealCards()
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
      
        public string FinishGameCheck()
        {
            if (gameStatus == Constants.GAME_OVER && PlayerWon == true)
                return $"Вы выиграли !! Теперь ваш рейтинг повысился";
            else if (gameStatus == Constants.GAME_OVER && PlayerWon == false)
                return $"Вы проиграли :( Теперь ваш рейтинг снизился";
            return string.Empty;
        }
        public string CompleteRound(List<ICard> cards) // срабатывает при нажатии на кнопкку
        {
            string MessageLog = "";
            List<ICard> playerCards = cards;
            List<ICard> botCards = new List<ICard>();
            Bot bot = new Bot();
            Player player = new Player();
            ICard playerCreature = new Creature();
            ICard botCreature = new Creature();

            foreach (var p in players)
            {
                if (typeof(Bot).IsInstanceOfType(p))
                    bot = (Bot)p;
                else if (typeof(Player).IsInstanceOfType(p))
                    player = (Player)p;
            }
            botCards = bot.PutCardFromHandOnTheTable();

            MessageLog += $"Ваши карты: {describeCard.DescribeSelectedCards(playerCards)}";
            MessageLog += $"Карты соперника: {describeCard.DescribeSelectedCards(botCards)}";

            // отрабатывает логика с заклинаниями
            if (playerCards.Count == 2)
            {
                playerCreature = anylyzingMove.UseSpell(playerCards, player);
                MessageLog += $"Ваша карта существа после применения заклинания:\n{describeCard.DescribeCard(playerCreature)}";
            }

            if (botCards.Count == 2)
            {
                botCreature = anylyzingMove.UseSpell(botCards, bot);
                MessageLog += $"Карта существа соперника после применения заклинания:\n{describeCard.DescribeCard(botCreature)}";
            }

            // отрабатывает логика с начислением очков и отниманием здоровья
            if (player.IsAttack == true)
            {
                player.IsAttack = false;
                MessageLog += anylyzingMove.AnalyzingMove(playerCreature, botCreature, player, bot);
            }
            else
            {
                player.IsAttack = true;
                MessageLog += anylyzingMove.AnalyzingMove(botCreature, playerCreature, bot, player);
            }

            CounterOfRounds++;

            if (CounterOfRounds == Constants.ROUNDS_MAX_COUNT)
               finishGame.FinishGame(Constants.ROUNDS_NUM_ENDED, player, bot, gameStatus, PlayerWon);
            else if (player.GetHealth() == 0)
                finishGame.FinishGame(Constants.PLAYER_DIED, player, bot, gameStatus, PlayerWon);
            else if (bot.GetHealth() == 0)
                finishGame.FinishGame(Constants.BOT_DIED, player, bot, gameStatus, PlayerWon);

            return MessageLog;
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
