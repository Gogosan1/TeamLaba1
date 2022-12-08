using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabaCreatedWithTeamWork.Cards;

namespace Model.Players_logic
{
    public interface IPlayer : ICardsInHands, IPlayerForAnalyzingMove, IPlayerForFinishGame
    {
        public string NickName { get; init; }
    }

    public interface IPlayerForAnalyzingMove : IAddHealth, IPutCardFromHandOnTheTable
    {
        public bool IsAttack { get; set; }
        void AddPointsPerGame();
        ICard ChooseCardFromHand(List<ICard> cards);
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

    public interface ITakeTheCardInHands//????
    {
        void TakeTheCardInHands(ICard card);
    }


    public interface IAddHealth
    {
        void AddHealth(ICard card);
    }

    public interface IPutCardFromHandOnTheTable
    {
        public List<ICard> PutCardFromHandOnTheTable();
    }
}
