using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modlel.Cards;

namespace LabaCreatedWithTeamWork.Players_logic
{
    
    public interface IPlayerForAnalyzingMove : IAddHealth
    {
        public bool IsAttack { get; set; }
        void AddPointsPerGame();
        public void ReduceHealth(ICard card);
    }

    public interface IPlayerForFinishGame
    {
        int GetHealth();
        int GetPointsPerGame();
        public int GlobalRating { get; set; }
    }
    public interface ICardsInHands
    {
        public List<ICard> CardsInHands { get; }
    }

    public interface ITakeTheCardInHands
    {
        void TakeTheCardInHands(ICard card);
    }


    public interface IAddHealth
    {
        void AddHealth(ICard card);
    }


    public interface IisAttack
    {
        bool IsAttack { get; set; }
    }
    public interface IPlayerForFinishingGame : IGetPointsPerGame
    {
        int GlobalRating { get; set; }
        public string NickName { get; init; }
    }
    public interface IGetPointsPerGame
    {
        int GetPointsPerGame();
    }
}
