using Model.Cards;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Reflection;
using WpfApp1;

namespace Model.Players_logic
{
    public class Player : IPlayer
    {
        public string NickName { get; init; }
        public List<ICard> CardsInHands { get; init; } // List of 8 cards or more
        public int GlobalRating { get; set; }
        public bool IsAttack { get; set; } 

        private int health;
        private int pointsPerGame;


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

        public ICard ChooseCardFromHand()
        {
            // в этом методе должна быть реализованна проверка на radioBatton
            // из датагрида по радиобаттон получаем карту


            return card;


            // должно быть обращение к форме, то есть необходимо взаимодействие с modelView 
            Console.WriteLine("Выберите карту из списка ваших карт по имени");
            foreach (var card in cards)
            {
                if (typeof(Creature).IsInstanceOfType(card))
                    Console.WriteLine($"Существо :{card.Name}");
                else
                    Console.WriteLine($"Заклинание :{card.Name}");
            }

            while (true)
            {
                // валидация реализовать
               // string name = Console.ReadLine();

                foreach (var card in cards)
                {
                    if (card.Name == name)
                        return card;
                }
            }
        }

        public List<ICard> PutCardFromHandOnTheTable()
        {
            List<ICard> cardsFromMove = new List<ICard>();
            ICard cardFromMove;

            while (true)
            {
                cardFromMove = (ChooseCardFromHand(CardsInHands));
                if (typeof(Creature).IsInstanceOfType(cardFromMove) || (typeof(HealsPlayerSpell).IsInstanceOfType(cardFromMove)))
                    break;
                else
                    // опять обращение ко viewModel
                    Console.WriteLine("Ошибка! Сначала вы можете выбрать только существо или лечащее заклинание");
            }

            CardsInHands.Remove(cardFromMove);
            cardsFromMove.Add(cardFromMove);

            return cardsFromMove;
                
            // реализовать проверку остались ли в колоде карты заклинаний

            return cardsFromMove;
            
        }
    }
}
