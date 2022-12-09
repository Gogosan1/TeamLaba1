using Model.Cards;
using Model.Players_logic;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Web;
using LabaTeam1.Model.InternalLogic;

namespace Model.InternalLogic
{
    interface IAnylyzingMove
    {
        string AnalyzingMove(ICard attackingCard, ICard defendingCard, IPlayerForAnalyzingMove attackingPlayer, IPlayerForAnalyzingMove defendingPlayer)
        {
            return String.Empty;
        }
        ICard UseSpell(List<ICard> Cards, IAddHealth playerOrBot);
    }
    public class AnalyzeMove : IAnylyzingMove
    {
        private string WhoseCardWon(IPlayerForAnalyzingMove player)
        {
            if (typeof(Player).IsInstanceOfType(player))
                return "ваша карта";
            else
                return "карта соперника";
        }
        public ICard UseSpell(List<ICard> Cards, IAddHealth playerOrBot)
        {
            if (typeof(ImprovesPowerSpell).IsInstanceOfType(Cards[1]))
                Cards[0].Power += Cards[1].Power;
            else if (typeof(ImprovesProtectionSpell).IsInstanceOfType(Cards[1]))
                ((ICreature)Cards[0]).Health += Cards[1].Power;
            else if (typeof(HealsPlayerSpell).IsInstanceOfType(Cards[1]))
                playerOrBot.AddHealth(Cards[1]);
            return Cards[0];
        }
        public string AnalyzingMove(ICard attackingCard, ICard defendingCard, IPlayerForAnalyzingMove attackingPlayer, IPlayerForAnalyzingMove defendingPlayer)
        {
            string info = "В этом сражении победила ";
            bool DamageIsMoreHealth = attackingCard.Power > ((ICreature)defendingCard).Health;
            bool DamageIsLessHealth = attackingCard.Power < ((ICreature)defendingCard).Health;
            bool DamageIsEqualsHealth = attackingCard.Power == ((ICreature)defendingCard).Health;

            if (DamageIsMoreHealth)
            {
                attackingCard.Power -= ((ICreature)defendingCard).Health;
                defendingPlayer.ReduceHealth(attackingCard);
                attackingPlayer.AddPointsPerGame();
                info += WhoseCardWon(attackingPlayer);
            }

            else if (DamageIsLessHealth)
            {
                defendingPlayer.AddPointsPerGame();
                info += WhoseCardWon(defendingPlayer);
            }

            else if (DamageIsEqualsHealth)
            {
                defendingPlayer.AddPointsPerGame();
                attackingPlayer.AddPointsPerGame();
                info += "дружба";
            }
            return info;
        }
    }
}
