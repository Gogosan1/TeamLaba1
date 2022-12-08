using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Players_logic
{
    public interface IPlayer
    {
        public List<ICard> CardsInHands { get; init; }
        public void TakeTheCardInHands(ICard card)
        {
            CardsInHands.Add(card);
        }

        public int GetPointsPerGame();
        public int GetHealth();
    }
}
