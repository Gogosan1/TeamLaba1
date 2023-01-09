using LabaCreatedWithTeamWork.Players_logic;
using Model.Cards;
using Model.Players_logic;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using WpfApp1;

namespace Model.InternalLogic
{
    public class Game
    {
        private DataManager dm;
        private List<IPlayer> playersOfOneGame; // 2 players Player and Bot
        private int CounterOfRounds;
        private string gameStatus = "";
        private bool PlayerWon = false;

        public Game(string NameOfPlayer)
        {
            dm = new DataManager();
            playersOfOneGame = new List<IPlayer>();
            AddPlayersForGame(NameOfPlayer);
            CounterOfRounds = 0;
        }
        public void AddPlayersForGame(string name) 
        {
            var bot = new Bot(dm.AllCards);
            playersOfOneGame.Add(bot);

            bool playerAdded = false;
            foreach (Player p in dm.AllPlayers)
            {
                if (String.Equals(p.NickName, name))
                {
                    playersOfOneGame.Add(p);
                    p.CardsInHands = DealCards();
                    playerAdded = true;
                    break;
                }
            }
            if (!playerAdded)
            {
                Player player = new Player(0, name);
                player.CardsInHands = DealCards();
                playersOfOneGame.Add(player);
            }

        }

        public bool IsPlayerExist(string inputNickName)
        {
            bool IsOldPlayer = false;
            foreach (Player p in dm.AllPlayers)
            {
                if (String.Equals(p.NickName,inputNickName))
                {
                    IsOldPlayer = true;
                    break;
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

        private ICard UseSpell(List<ICard> Cards, IPlayer playerOrBot)
        {
            if (typeof(ImprovesPowerSpell).IsInstanceOfType(Cards[1]))
                Cards[0].Power += Cards[1].Power;
            else if (typeof(ImprovesProtectionSpell).IsInstanceOfType(Cards[1]))
                Cards[0].Health += Cards[1].Power;
            else if (typeof(HealsPlayerSpell).IsInstanceOfType(Cards[1]))
                playerOrBot.AddHealth(Cards[1]);
            return Cards[0];
        }
        private string DescribeCard(ICard card)
        {
            if (typeof(Creature).IsInstanceOfType(card))
                return $"Имя: {card.Name}, сила: {card.Power}, здоровье: {(card).Health}";
            else return $"Имя: {card.Name}, сила: {card.Power}";
        }
        private string DescribeSelectedCards(List<ICard> cards)
        {
            string describe = "";
            foreach (var card in cards)
            {
                if (typeof(Creature).IsInstanceOfType(card))
                    describe += $"Существо:\n{DescribeCard(card)}\n";
                else if (typeof(ImprovesPowerSpell).IsInstanceOfType(card))
                    describe += $"Улучшающее силу заклинание:\n{DescribeCard(card)}\n";
                else if (typeof(ImprovesProtectionSpell).IsInstanceOfType(card))
                    describe += $"Улучшающее здоровье существа заклинание:\n{DescribeCard(card)}\n";
                else if (typeof(HealsPlayerSpell).IsInstanceOfType(card))
                    describe += $"Улучшающее здоровье игрока заклинание:\n{DescribeCard(card)}\n";
            }
            return describe;
        }
        private string WhoseCardWon(IPlayerForAnalyzingMove player)
        {
            if (typeof(Player).IsInstanceOfType(player))
                return "ваша карта";
            else
                return "карта соперника";
        }

        public string AnalyzeMove(ICard attackingCard, ICard defendingCard, IPlayerForAnalyzingMove attackingPlayer, IPlayerForAnalyzingMove defendingPlayer)
        {
            string info = "В этом сражении победила ";
            bool DamageIsMoreHealth = attackingCard.Power > (defendingCard).Health;
            bool DamageIsLessHealth = attackingCard.Power < (defendingCard).Health;
            bool DamageIsEqualsHealth = attackingCard.Power == (defendingCard).Health;

            if (DamageIsMoreHealth)
            {
                attackingCard.Power -= (defendingCard).Health;
                defendingPlayer.ReduceHealth(attackingCard);
                attackingPlayer.AddPointsPerGame();
                info += WhoseCardWon(attackingPlayer);
            }

            else if (DamageIsLessHealth)
            {
                defendingPlayer.AddPointsPerGame();
                info += WhoseCardWon(defendingPlayer);
            }

            else if (DamageIsEqualsHealth)
            {
                defendingPlayer.AddPointsPerGame();
                attackingPlayer.AddPointsPerGame();
                info += "дружба";
            }
            return info;
        }
        private void FinishGame(string whyFinishGame, IPlayerForFinishingGame player, IGetPointsPerGame bot)
        {
            if (whyFinishGame == Constants.ROUNDS_NUM_ENDED)
            {
                if (player.GetPointsPerGame() > bot.GetPointsPerGame())
                    PlayerWon = true;
                gameStatus += Constants.GAME_OVER;
            }
            else if (whyFinishGame == Constants.PLAYER_DIED)
            {
                PlayerWon = false;
                gameStatus += Constants.GAME_OVER;
            }
            else if (whyFinishGame == Constants.BOT_DIED)
            {
                PlayerWon = true;
                gameStatus += Constants.GAME_OVER;
            }

            if (PlayerWon == true)
            {
                player.GlobalRating += player.GetPointsPerGame();
            }
            else
            {
                player.GlobalRating -= Constants.LOSS_OF_POINTS;
            }
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

            foreach (var p in playersOfOneGame)
            {
                if (typeof(Bot).IsInstanceOfType(p))
                    bot = (Bot)p;
                else if (typeof(Player).IsInstanceOfType(p))
                    player = (Player)p;
            }
            botCards = bot.PutCardFromHandOnTheTable();

            MessageLog += $"Ваши карты: {DescribeSelectedCards(playerCards)}";
            MessageLog += $"Карты соперника: {DescribeSelectedCards(botCards)}";

            // отрабатывает логика с заклинаниями
            if (playerCards.Count == 2)
            {
                playerCreature = UseSpell(playerCards, player);
                MessageLog += $"Ваша карта существа после применения заклинания:\n{DescribeCard(playerCreature)}";
            }

            if (botCards.Count == 2)
            {
                botCreature = UseSpell(botCards, bot);
                MessageLog += $"Карта существа соперника после применения заклинания:\n{DescribeCard(botCreature)}";
            }

            // отрабатывает логика с начислением очков и отниманием здоровья
            if (player.IsAttack == true)
            {
                player.IsAttack = false;
                MessageLog += AnalyzeMove(playerCreature, botCreature, player, bot);
            }
            else
            {
                player.IsAttack = true;
                MessageLog += AnalyzeMove(botCreature, playerCreature, bot, player);
            }

            CounterOfRounds++;

        /*    if (CounterOfRounds == Constants.ROUNDS_MAX_COUNT)
                FinishGame(Constants.ROUNDS_NUM_ENDED, player, bot);
            else if (player.GetHealth() == 0)
                FinishGame(Constants.PLAYER_DIED, player, bot);
            else if (bot.GetHealth() == 0)
                FinishGame(Constants.BOT_DIED, player, bot);
*/
            return MessageLog;
        }


        /*  public string CompleteRound(List<ICard> cards) // срабатывает при нажатии на кнопкку
          {
              string MessageLog = "";
              // либо сделать через возврат логов либо через подписку
              List<ICard> playerCards = cards;
              Bot bot = new Bot();
              foreach (var player in playersOfOneGame)
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

              if (CounterOfRounds == 12  или кто-то умер...)
                  throw new Exception("Игра закончена");

              return null; // возвращаем большое сообщение что произошло
          }*/

        public List<Player> GetListOfPlayers() => dm.AllPlayers;

        public List<ICard> GetListOfPlayersCardsInGame()
        {
            foreach(var player in playersOfOneGame)
                if (typeof(Player).IsInstanceOfType(player))
                    return player.CardsInHands;
            return null;
        }

        public int GetHealth(string type)
        {
            foreach ( var player in playersOfOneGame)
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
            foreach (var player in playersOfOneGame)
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
