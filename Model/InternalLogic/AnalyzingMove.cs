/*using LabaCreatedWithTeamWork.Players_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabaCreatedWithTeamWork.Cards;
using System.Numerics;
using static LabaCreatedWithTeamWork.Cards.Spell;

namespace Model.InternalLogic
{
    interface IConductRound
    {
        void AnalyzeMove(IPlayerForAnalyzingMove player, IBotForAnalyzingMove bot);
        void AddCardToPlayer();
    }

    public class ConductRound : IConductRound
    {
        AnalyzeMove(IPlayerForAnalyzingMove player, IBotForAnalyzingMove bot)
        {
             ICard attackingCard = new Creature();
             ICard defendingCard = new Creature();
        }
    }























        //private List<ICard> playerCards = new List<ICard>();
        //private List<ICard> botCards = new List<ICard>(); 
        private ICard attackingCard = new Creature();
        private ICard defendingCard = new Creature();
        //private ICard playerCreatureCard = new Creature();
        //private ICard botCreatureCard = new Creature();
        private ICard playerSpell = new Spell();
        private ICard botSpell = new Spell();
        private void IdentifyRolesOfCards(IPutCardFromHandOnTheTable player, IPutCardFromHandOnTheTable bot, List<ICard> playerCards, List<ICard> botCards)
        {
            playerCards = player.PutCardFromHandOnTheTable();
            botCards = bot.PutCardFromHandOnTheTable();

            
            //if (playerCards.Count == 1)
            //    playerCreatureCard = playerCards[0];
            //else
            //    foreach (var c in playerCards)
            //    {
            //        if (c is Spell)
            //            playerSpell = c;
            //        else if (c is Creature)
            //        playerCreatureCard = c;
            //    }
            //if (botCards.Count == 1)
            //    botCreatureCard = botCards[0];
            //else
            //    foreach (var c in botCards)
            //    {
            //        if (c is Spell)
            //            botSpell = c;
            //        else if (c is Creature)
            //            botCreatureCard = c;
            //    }
            foreach (var c in player.PutCardFromHandOnTheTable())
            {
                if(typeof(Spell).IsInstanceOfType(c))
                    UseSpells(player,c)
            }
        }
        private void UseSpells(IAddHealth player, IAddHealth bot, ICard playerSpell, ICard botSpell, ICard playerCreatureCard, ICard botCreatureCard)
        {
            if (playerSpell.Type == SpellType.ImprovesPower)
                playerCreatureCard.Power += playerSpell.Power;
            else if (playerSpell.Type == SpellType.ImprovesProtection)
                playerCreatureCard.Health += playerSpell.Power;
            else if (playerSpell.Type == SpellType.HealsPlayer)
                player.AddHealth(playerSpell);

            if (botSpell.Type == SpellType.ImprovesPower)
                botCreatureCard.Power += botSpell.Power;
            else if (botSpell.Type == SpellType.ImprovesProtection)
                botCreatureCard.Health += botSpell.Power;
            else if (botSpell.Type == SpellType.HealsPlayer)
                bot.AddHealth(botSpell);
        }
        private void UseSpells(IAddHealth bot, ICard playerSpell, ICard botSpell, ICard playerCreatureCard, ICard botCreatureCard)
        {
            if (playerSpell.Type == SpellType.ImprovesPower)
                playerCreatureCard.Power += playerSpell.Power;
            else if (playerSpell.Type == SpellType.ImprovesProtection)
                playerCreatureCard.Health += playerSpell.Power;
            else if (playerSpell.Type == SpellType.HealsPlayer)
                player.AddHealth(playerSpell);

            if (botSpell.Type == SpellType.ImprovesPower)
                botCreatureCard.Power += botSpell.Power;
            else if (botSpell.Type == SpellType.ImprovesProtection)
                botCreatureCard.Health += botSpell.Power;
            else if (botSpell.Type == SpellType.HealsPlayer)
                bot.AddHealth(botSpell);
        }

        public void AnalyzeMove(IPlayerForAnalyzingMove player, IBotForAnalyzingMove bot)
        {
            IdentifyRolesOfCards(player, bot, playerCards, botCards);
            UseSpells(player, bot, playerSpell, botSpell, playerCreatureCard, botCreatureCard);

            bool DamageIsMoreHealth = attackingCard.Power > defendingCard.Health;
            bool DamageIsLessHealth = attackingCard.Power < defendingCard.Health;
            bool DamageIsEqualsHealth = attackingCard.Power == defendingCard.Health;

            if (player.IsAttack == true)// - игрок атакует
            {
                player.IsAttack = false;
                attackingCard = playerCreatureCard;
                defendingCard = botCreatureCard;

                if (DamageIsMoreHealth)
                {
                    bot.health -= attackingCard.Power - defendingCard.Health;
                    player.AddPointsPerGame(); //договориться о системе начислений
                }

                else if (DamageIsLessHealth)
                {
                    defendingCard.Health -= attackingCard.Power;
                    bot.AddPointsPerGame();
                }

                else if (DamageIsEqualsHealth)
                {
                    player.AddPointsPerGame();
                    bot.AddPointsPerGame();
                }
            }

            else
            {
                attackingCard = botCreatureCard;
                defendingCard = playerCreatureCard;
                player.IsAttack = true;

                if (DamageIsMoreHealth)
                {
                    player.health -= attackingCard.Power - defendingCard.Health;
                    bot.AddPointsPerGame(); //договориться о системе начислений
                }

                else if (DamageIsLessHealth)
                {
                    defendingCard.Health -= attackingCard.Power;
                    player.AddPointsPerGame();
                }

                else if (DamageIsEqualsHealth)
                {
                    player.AddPointsPerGame();
                    bot.AddPointsPerGame();
                }
            }
        }
    }
}
*/