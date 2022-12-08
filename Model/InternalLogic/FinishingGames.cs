/*using LabaCreatedWithTeamWork.Players_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Model.InternalLogic
{
    interface IFinishGame
    {
        void FinishGame(IPlayerForFinishGame player, IBotForFinishGame bot);
    }

    public class FinishingGames: IFinishGame
    {
        public void FinishGame(IPlayerForFinishGame player, IBotForFinishGame bot)
        {
            bool PlayerDied = player.GetHealth() == 0;
            bool botDied = bot.GetHealth() == 0;
            bool impossibleToMakeMove;
            
            if (PlayerDied)
            {
                player.GlobalRating -= Constants.LOSS_OF_POINTS;
                
                if (player.GlobalRating < 0)
                    player.GlobalRating = 0;
            }
            else if (botDied)
                player.GlobalRating += player.GetPointsPerGame();
            else if (impossibleToMakeMove)
            {
                if (player.GetPointsPerGame() == bot.GetPointsPerGame())
                    player.GlobalRating += player.GetPointsPerGame() / 2;
                if (player.GetPointsPerGame() > bot.GetPointsPerGame())
                    player.GlobalRating += player.GetPointsPerGame();
                else
                {
                    player.GlobalRating -= Constants.LOSS_OF_POINTS;

                    if (player.GlobalRating < 0)
                        player.GlobalRating = 0;
                }
            }
        }
    }
}
*/