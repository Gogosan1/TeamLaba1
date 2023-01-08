﻿using Model.Cards;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using WpfApp1;

namespace Model.Players_logic
{
    public class Player : IPlayer
    {
        [JsonInclude]
        public int GlobalRating { get; set; }
        [JsonInclude]
        public string NickName { get; init; }
        [JsonIgnore]
        public bool IsAttack { get; set; }
        [JsonIgnore]
        public List<ICard> CardsInHands { get; init; } // List of 8 cards or more

        private int health;
        private int pointsPerGame;

        [JsonConstructor]
        public Player(int globalRating, string name)
        {
            GlobalRating = globalRating;
            NickName = name;
            CardsInHands = new List<ICard>();
            IsAttack = true;
            health = Constants.DEFAULT_HEALTH;
            pointsPerGame = 0;
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

        public void AddHealth(ICard card)//передаем сюда spell
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
