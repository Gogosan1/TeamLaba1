
using Model.Cards;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Reflection;
using WpfApp1;

namespace Model.Players_logic
{
    public class Bot : IPlayer
    {
        public List<ICard> CardsInHands { get; set; }
        public bool IsAttack { get; set; }
        public int GlobalRating { get; set; }

        private int health;
        private int pointsPerGame;

        public Bot(List<ICard> cardsOfOneGame)
        {
            CardsInHands = cardsOfOneGame;
            pointsPerGame = 0;
            health = Constants.DEFAULT_HEALTH; 
        }
        public Bot() { }

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

        private ICard ChooseCardFromHand()
        {
            Random random = new Random();
            int index = random.Next(CardsInHands.Count);
            var card = CardsInHands[index];
            return card;
        }

        public List<ICard> PutCardFromHandOnTheTable()
        {
            List<ICard> cardsFromMove = new List<ICard>();
            ICard cardFromMove;

            while (true)
            {
                cardFromMove = (ChooseCardFromHand());
                if (typeof(Creature).IsInstanceOfType(cardFromMove))
                {
                    cardsFromMove.Add(cardFromMove);
                    break;
                }

            }


            Random random = new Random();
            int value = random.Next(0, 1);

            if (value == 1)
                while (true)
                {
                    cardFromMove = (ChooseCardFromHand());
                    if (typeof(ImprovesPowerSpell).IsInstanceOfType(cardFromMove) ||
                        typeof(ImprovesProtectionSpell).IsInstanceOfType(cardFromMove) ||
                        (typeof(HealsPlayerSpell).IsInstanceOfType(cardFromMove)))
                    {
                        cardsFromMove.Add(cardFromMove);
                        break;
                    }
                }


            return cardsFromMove;
        }
    }
}
