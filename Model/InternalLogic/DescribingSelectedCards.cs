using Model.Cards;
using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaTeam1.Model.InternalLogic
{
    public interface IDescribeCard
    {
        string DescribeCard(ICard card);
        string DescribeSelectedCards(List<ICard> cards);
    }
    internal class DescribingSelectedCards
    {
        public string DescribeCard(ICard card)
        {
            if (typeof(Creature).IsInstanceOfType(card))
                return $"Имя: {card.Name}, сила: {card.Power}, здоровье: {((ICreature)card).Health}";
            else return $"Имя: {card.Name}, сила: {card.Power}";
        }
        public string DescribeSelectedCards(List<ICard> cards)
        {
            string describe = "";
            foreach (var card in cards)
            {
                if (typeof(Creature).IsInstanceOfType(card))
                    describe += $"Существо:\n{DescribeCard(card)}\n";
                else if (typeof(ImprovesPowerSpell).IsInstanceOfType(card))
                    describe += $"Улучшающее силу заклинание:\n{DescribeCard(card)}\n";
                else if (typeof(ImprovesProtectionSpell).IsInstanceOfType(card))
                    describe += $"Улучшающее здоровье существа заклинание:\n{DescribeCard(card)}\n";
                else if (typeof(HealsPlayerSpell).IsInstanceOfType(card))
                    describe += $"Улучшающее здоровье игрока заклинание:\n{DescribeCard(card)}\n";
            }
            return describe;
        }
    }
}
