
using Model.Cards;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;
using WpfApp1;
namespace Model.Players_logic

{
    public class Player : IPlayer
    {
        public int GlobalRating { get; set; }
        public string NickName { get; init; }

        [JsonIgnore]
        public bool IsAttack { get; set; }
        [JsonIgnore]
        public List<ICard> CardsInHands { get; set; }

        private int health;
        private int pointsPerGame;

        [JsonConstructor]
        public Player(int GlobalRating, string NickName)
        {
            this.NickName = NickName;
            CardsInHands = new List<ICard>();
            pointsPerGame = 0;
            this.GlobalRating = GlobalRating;
            health = Constants.DEFAULT_HEALTH;
            IsAttack = true;
        }
        public Player()
        { }
        
        public void TakeTheCardInHands(ICard card) 
        {
          CardsInHands.Add(card);
        }
        public void AddPointsPerGame()
        {
            pointsPerGame += Constants.POINTS_FOR_WINNING_ROUD;
        }
        public int GetPointsPerGame() => pointsPerGame;

        public void AddHealth(ICard card)
        {
            health += card.Power;
        }
        public void ReduceHealth(ICard card)
        {
            health -= card.Power;
            if (health < 0)
                health = 0;
        }
        public int GetHealth() => health;    
    }
}
