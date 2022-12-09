using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Players_logic
{
    public interface IPlayer: IGetPointsPerGame
    {
        public List<ICard> CardsInHands { get; init; }
        public void TakeTheCardInHands(ICard card);
        int GetHealth();
    }

    public interface IAddHealth
    {
        void AddHealth(ICard card);
    }

    public interface IPlayerForAnalyzingMove
    {
        void AddPointsPerGame();
        void ReduceHealth(ICard card);
    }
    public interface IisAttack
    {
        bool IsAttack { get; set; }
    }
    public interface IPlayerForFinishingGame : IGetPointsPerGame
    {
        int GlobalRating { get; set; }
    }
    public interface IGetPointsPerGame
    {
        int GetPointsPerGame();
    }
}
