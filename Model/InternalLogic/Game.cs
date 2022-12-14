using LabaCreatedWithTeamWork.Players_logic;
using LabaTeam1.Model.Cards;
using Model.Cards;
using Model.Players_logic;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Windows.Controls;
using WpfApp1;

namespace Model.InternalLogic
{
    public class Game
    {       
        public Game(string NameOfPlayer)
        {
            dm = new DataManager();
            playersOfOneGame = new List<IPlayer>();
            AddPlayersForGame(NameOfPlayer);
            CounterOfRounds = 0;
            PlayerWon = false;
            IsGameOver = false;
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
            if (playerAdded == false)
            {
                Player player = new Player(0, name);
                player.CardsInHands = DealCards();
                playersOfOneGame.Add(player);
                dm.AddPlayer(player);
            }

        }      
        
        private List<ICard> DealCards ()
        {
            List<ICard> cardsForOneGame = new List<ICard>();

            int creaturesCount = 0;
            for (int i = 0; i < 6; i++)
            {
                Random rand = new Random();

                int index = rand.Next(dm.AllCards.Count);

                if (typeof(Creature).IsInstanceOfType(dm.AllCards[index]))
                    creaturesCount++;

                if( i == 5 && creaturesCount == 0)
                    while (true)
                    {
                        index = rand.Next(dm.AllCards.Count);
                        if (typeof(Creature).IsInstanceOfType(dm.AllCards[index]))
                            break;
                    }

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
                return $"??????: {card.Name}, ????????: {card.Power}, ????????????????: {(card).Health}";
            else return $"??????: {card.Name}, ????????: {card.Power}";
        }
        private string DescribeSelectedCards(List<ICard> cards)
        {
            string describe = "";
            foreach (var card in cards)
            {
                if (typeof(Creature).IsInstanceOfType(card))
                    describe += $"\n????????????????:\n{DescribeCard(card)}\n";
                else if (typeof(ImprovesPowerSpell).IsInstanceOfType(card))
                    describe += $"???????????????????? ???????? ????????????????????:\n{DescribeCard(card)}\n";
                else if (typeof(ImprovesProtectionSpell).IsInstanceOfType(card))
                    describe += $"???????????????????? ???????????????? ???????????????? ????????????????????:\n{DescribeCard(card)}\n";
                else if (typeof(HealsPlayerSpell).IsInstanceOfType(card))
                    describe += $"???????????????????? ???????????????? ???????????? ????????????????????:\n{DescribeCard(card)}\n";
            }
            return describe;
        }
        private string WhoseCardWon(IPlayerForAnalyzingMove player)
        {
            if (typeof(Player).IsInstanceOfType(player))
                return "???????? ?????????? ?? ???? ???????????????????? 10 ??????????. \n..???????????????? ?????????? ?????? ???????????????????? ????????..";
            else
                return "?????????? ?????????????????? ?? ???? ?????????????????? 10 ??????????. \n..???????????????? ?????????? ?????? ???????????????????? ????????..";
        }
        private string AnalyzeMove(ICard attackingCard, ICard defendingCard, IPlayerForAnalyzingMove attackingPlayer, IPlayerForAnalyzingMove defendingPlayer)
        {
            string info = "?? ???????? ???????????????? ???????????????? ";
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
                info += "????????????. \n..???????????????? ?????????? ?????? ???????????????????? ????????..";
            }
            return info;
        }
        private void FinishGame(string whyFinishGame, Player player, IGetPointsPerGame bot)
        {
            IsGameOver = true;

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
                foreach (var p in dm.AllPlayers)
                {
                    if (p.NickName == player.NickName)
                        player.GlobalRating += p.GetPointsPerGame();
                }
            }
            else
            {
                foreach (var p in dm.AllPlayers)
                {
                    if (p.NickName == player.NickName)
                        player.GlobalRating -= Constants.LOSS_OF_POINTS;
                    if(player.GlobalRating < 0)
                        player.GlobalRating = 0;
                }
            }

            dm.JsonSerializePlayers();
            dm.DeserealizePlayers();
        }
        public string FinishGameInfo()
        {
            string info = "???????? ????????????????: ";
            if (CounterOfRounds == Constants.ROUNDS_MAX_COUNT)
                info += "?????????????????????? ?????????? ??????????????";
            else if (playersOfOneGame[1].GetHealth() <= 0)
                info += "?????????????????????? ???????? ????????????????";
            else if (playersOfOneGame[0].GetHealth() <= 0)
                info += "?????????????????????? ???????????????? ??????????????????";

            if (gameStatus == Constants.GAME_OVER && PlayerWon == true)
                info += $"\n???? ???????????????? !! ???????????? ?????? ?????????????? ?????????????????? ???? {playersOfOneGame[1].GetPointsPerGame()} ??????????";
            else if (gameStatus == Constants.GAME_OVER && PlayerWon == false)
                info += $"\n???? ?????????????????? :( ???????????? ?????? ?????????????? ???????????????? ???? 100 ??????????";
            return info;
        }

        public string CompleteRound(List<ICard> playerCards) 
        {
            string MessageLog = "";
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

            foreach (var card in playerCards)
                if (typeof(Creature).IsInstanceOfType(card))
                    playerCreature = card;

            foreach (var card in botCards)
                if (typeof(Creature).IsInstanceOfType(card))
                    botCreature = card;

            MessageLog += $"???????? ??????????: {DescribeSelectedCards(playerCards)}";
            MessageLog += $"\n?????????? ??????????????????: {DescribeSelectedCards(botCards)}";

            if (playerCards.Count == 2)
            {
                ICard card = new Creature();
                card.Power = playerCreature.Power;
                card.Health = playerCreature.Health;
                playerCreature = UseSpell(playerCards, player);

                if (card.Health == playerCreature.Health && card.Power == playerCreature.Power)
                    MessageLog += "\n???????? ?????????????????? ????????????????????, ???????????????????? ???????? ????????????????";
                else
                    MessageLog += $"\n???????? ?????????? ???????????????? ?????????? ???????????????????? ????????????????????:\n{DescribeCard(playerCreature)}";
            }

            if (botCards.Count == 2)
            {
                ICard card = new Creature();
                card.Power = botCreature.Power;
                card.Health = botCreature.Health;

                botCreature = UseSpell(botCards, bot);
                if (card.Health == playerCreature.Health && card.Power == playerCreature.Power)
                    MessageLog += "\n???????? ?????????????????? ????????????????????, ???????????????????? ???????????????? ??????????????????";
                else
                    MessageLog += $"\n?????????? ???????????????? ?????????????????? ?????????? ???????????????????? ????????????????????:\n{DescribeCard(botCreature)}";
            }

            if (player.IsAttack == true)
            {
                player.IsAttack = false;
                MessageLog += "\n???? ????????????????!\n";
                MessageLog += AnalyzeMove(playerCreature, botCreature, player, bot);
            }
            else
            {
                player.IsAttack = true;
                MessageLog += "\n???? ??????????????????????!\n";
                MessageLog += AnalyzeMove(botCreature, playerCreature, bot, player);
            }

            CounterOfRounds++;

            if (CounterOfRounds == Constants.ROUNDS_MAX_COUNT)
                FinishGame(Constants.ROUNDS_NUM_ENDED, player, bot);
            else if (player.GetHealth() <= 0)
                FinishGame(Constants.PLAYER_DIED, player, bot);
            else if (bot.GetHealth() <= 0)
                FinishGame(Constants.BOT_DIED, player, bot);

            DeletePlayersCardFromTheDeck(playerCards);
            AddCardsToThePlayersDeck(playerCards.Count);

            return MessageLog;
        }

        private void DeletePlayersCardFromTheDeck(List<ICard> cards)
        {
            var player = GetPlayerOfTheGame();
            foreach (var takedCards in cards)
                for (int i = 0; i < player.CardsInHands.Count; i++)
                    if (String.Equals(player.CardsInHands[i].Name, takedCards.Name))
                    {
                        player.CardsInHands.Remove(player.CardsInHands[i]);
                        i--;
                    }
        }

        private void AddCardsToThePlayersDeck(int countOfRemovedCards)
        {
            int countOfCreatures = 0;
            int countOfAddedCards = 0;
            var player = GetPlayerOfTheGame();

            foreach (var card in player.CardsInHands)
            {
                if (typeof(Creature).IsInstanceOfType(card))
                    countOfCreatures++;
            }

            if (countOfCreatures == 0)
            {
                while (true)
                {
                    var newCard = ChooseTheNewCardFromAllCardsOfGame();
                    if (typeof(Creature).IsInstanceOfType(newCard))
                    {
                        player.CardsInHands.Add(newCard);
                        countOfAddedCards++;
                        break;
                    }
                }

            }

            while (countOfRemovedCards != countOfAddedCards)
            {
                var newCard1 = ChooseTheNewCardFromAllCardsOfGame();
                player.CardsInHands.Add(newCard1);
                countOfAddedCards++;


            }

        }
        private ICard ChooseTheNewCardFromAllCardsOfGame()
        {
            Random random = new Random();
            int index = random.Next(dm.AllCards.Count);
            var newCard = dm.AllCards[index];
            return newCard;
        }
        public List<Player> GetListOfPlayers() => dm.AllPlayers;
       
        public List<ICard> GetListOfPlayersCardsInGame()
        {
            var player = GetPlayerOfTheGame();
            return player.CardsInHands;
        }
     
        public void ReduceGlobalRating()
        {
            var player = GetPlayerOfTheGame();
            foreach (var p in dm.AllPlayers)
                if (String.Equals(p.NickName, player.NickName))
                    foreach (var bot in playersOfOneGame)
                        if (typeof(Bot).IsInstanceOfType(bot))
                            p.GlobalRating -= bot.GetPointsPerGame();

            dm.JsonSerializePlayers();
            dm.DeserealizePlayers();
        }

        private Player GetPlayerOfTheGame()
        {
            foreach (var player in playersOfOneGame)
                if (typeof(Player).IsInstanceOfType(player))
                    return (Player)player;
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

        public bool IsGameOver { get; private set; }
        private DataManager dm;
        private List<IPlayer> playersOfOneGame;
        public int CounterOfRounds { get; private set; }
        private string gameStatus = "";
        private bool PlayerWon;
    }
}
