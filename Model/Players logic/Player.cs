using LabaTeam1.Model.InternalLogic;
using Model.Cards;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Model.Players_logic
{
    public class Player : IPlayer, IAddHealth, IisAttack, IPlayerForAnalyzingMove, IPlayerForFinishingGame, IGetPointsPerGame
    {
        [JsonInclude]
        public int GlobalRating { get; set; }
        [JsonInclude]
        public string NickName { get; init; }
  
        public bool IsAttack { get; set; } 
        public List<ICard> CardsInHands { get; init; }

        private int health;
        private int pointsPerGame;

        public Player()
        {
        }

        public Player (string nickName, List<ICard> cards, int globalRating)
        {
            NickName = nickName;
            CardsInHands = cards;
            pointsPerGame = 0;
            GlobalRating = globalRating;
            health = Constants.DEFAULT_HEALTH;
            IsAttack = true;
        }
        
        public void TakeTheCardInHands(ICard card) 
        {
          CardsInHands.Add(card);
        }
        public void AddPointsPerGame()
        {
            pointsPerGame += Constants.POINTS_FOR_WINNING;
        }
        public int GetPointsPerGame() => pointsPerGame;

        public void AddHealth(ICard card)
        {
            health += card.Power;
        }
        public void ReduceHealth(ICard card)
        {
            health -= card.Power;
        }
        public int GetHealth() => health;
    }
}
