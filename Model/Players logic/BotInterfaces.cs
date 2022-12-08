using LabaCreatedWithTeamWork.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Players_logic
{
    public interface IBot : ICardsInHands, IBotForAnalyzingMove, IBotForFinishGame
    {
    }
    public interface IBotForAnalyzingMove : IAddHealth, IPutCardFromHandOnTheTable
    {
        void AddPointsPerGame();
        ICard ChooseCardFromHand(List<ICard> cards);
        public void ReduceHealth(ICard card);
    }
    public interface IBotForFinishGame
    {
        int GetHealth();
        int GetPointsPerGame();
    }
}
