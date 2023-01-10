using LabaCreatedWithTeamWork.Players_logic;
using Model.Cards;
using Model.Players_logic;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using WpfApp1;

namespace Model.InternalLogic
{
    public class Game
    {
        // Прежде чем пытаться запустить проект, вставь папку Files в корень папки Debug
        // Проект собирается, если не соберётся напиши мне, созвонимся в дс я покажу, что не так
        // Когда сделаешь всю логику, нужно будет отладить. Чтобы проверить как все работает нужно будет раскомментировать
        //код в 2 файлах, в GameWindowPresenter 3 метода, и в GameWindow.xaml.cs чуть-чуть строк кода 82 - 89 строки  


        // В классе игра есть список игроков. В нём всегда только 2 игрока это бот и сам игрок.
        // Поэтому при окончании игры нужно изменять глобальный рейтинг по имени игрока в списке из класса датаменеджера
        // foreach( player in dm.AllPlayers)
        //      if (player.name == имени игрока в игре)
        //          изменить глобальный рейтинг игрока


        // Логику можешь реализовывать как угодно, я тебе на вход в метод CompleteRound буду передавать список из одной или 2 карт
        // На выходе из CompleteRound мне нужны только логи проведдённого раунда
        //
        // Не забывай: в конце каждого хода добирать столько карт на руку, сколько использовали во время игры,
        // старые карты удалять, либо возвращать в список если их не добили
        // 
        // Еще мне нужны 2 метода.
        //1) Один после каждого хода проверяет закончилась ли игра(тип bool), он мне должен возвращать истину или ложь.
        //2) Второй возвратит логи окончания игры: а именно, почему закончилась игра, кто победил,
        //на сколько рейтинг повышен или понижен, текущй рейтинг. Все это возвращать просто в строке.
        //
        // Рекомендуемые названия методов: 1)bool IsGameOver() 
        //                                 2)string GameOverMessage() <- вернёт строку 
        //Не глаголы конечно, но что есть


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

        //Здесь вся логика игры написанная тобой, я откатил изменения и закомментировал не рабочий код.
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
        private string AnalyzeMove(ICard attackingCard, ICard defendingCard, IPlayerForAnalyzingMove attackingPlayer, IPlayerForAnalyzingMove defendingPlayer)
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
                        p.GlobalRating += player.GetPointsPerGame();
                }
            }
            else
            {
                foreach (var p in dm.AllPlayers)
                {
                    if (p.NickName == player.NickName)
                        player.GlobalRating -= Constants.LOSS_OF_POINTS;
                }
            }
        }
        public string FinishGameInfo()
        {

            if (gameStatus == Constants.GAME_OVER && PlayerWon == true)
                return $"Вы выиграли !! Теперь ваш рейтинг повысился";
            else if (gameStatus == Constants.GAME_OVER && PlayerWon == false)
                return $"Вы проиграли :( Теперь ваш рейтинг снизился";
            return string.Empty;
        }

        // Взаимодействие с формой происходит только здесь, поэтому  методы игры выше, в целом могут быть приватными
        public string CompleteRound(List<ICard> playerCards) // срабатывает при нажатии на кнопкку
        {
            string MessageLog = "";
            //List<ICard> playerCards = playerCards;
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

            if (CounterOfRounds == Constants.ROUNDS_MAX_COUNT)
                FinishGame(Constants.ROUNDS_NUM_ENDED, player, bot);
            else if (player.GetHealth() <= 0)
                FinishGame(Constants.PLAYER_DIED, player, bot);
            else if (bot.GetHealth() <= 0)
                FinishGame(Constants.BOT_DIED, player, bot);

            DeletePlayersCardFromTheDeck(playerCards);
            AddCardsToThePlayersDeck(playerCards.Count);
            // здесь нужно обращатся к методам удаления и раздачи карт

            return MessageLog;
        }

        private void DeletePlayersCardFromTheDeck(List<ICard> cards)
        {
            foreach (var player in playersOfOneGame)
                if (typeof(Player).IsInstanceOfType(player))
                    foreach (var takedCards in cards)
                        for (int i = 0; i < player.CardsInHands.Count; i++)
                            if (String.Equals(player.CardsInHands[i].Name, takedCards.Name))
                            {
                                player.CardsInHands.Remove(player.CardsInHands[i]);
                                i--;
                            }
        }

        // вынести проверку на тип в отдельный метод
        private void AddCardsToThePlayersDeck(int countOfRemovedCards)
        {
            int countOfCreatures = 0;
            int countOfAddedCards = 0;
            foreach (var player in playersOfOneGame)
                if (typeof(Player).IsInstanceOfType(player))
                {
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
            foreach(var player in playersOfOneGame)
                if (typeof(Player).IsInstanceOfType(player))
                    return player.CardsInHands;
            return null;
        }
     
        
        // эти методы тоже используются в форме, для рендерига, их менять не нужно
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
        private List<IPlayer> playersOfOneGame; // 2 players Player and Bot
        private int CounterOfRounds;
        private string gameStatus = "";
        private bool PlayerWon;

    }
}
