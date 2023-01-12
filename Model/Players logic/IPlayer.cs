using LabaCreatedWithTeamWork.Players_logic;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Players_logic
{
    public interface IPlayer : ICardsInHands, IPlayerForAnalyzingMove, IGetPointsPerGame
    {
        public List<ICard> CardsInHands { get; set; }
        public void TakeTheCardInHands(ICard card)
        {
            CardsInHands.Add(card);
        }
        public void AddHealth(ICard card);
        public void ReduceHealth(ICard card);
        public int GetPointsPerGame();
        public int GetHealth();
    }
}
