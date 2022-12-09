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
    interface IFinishGame
    {
        void FinishGame(string whyFinishGame, IPlayerForFinishingGame player, IGetPointsPerGame bot, string gameStatus, bool PlayerWon);
    }

    public class FinishingGames: IFinishGame
    {
        public void FinishGame(string whyFinishGame, IPlayerForFinishingGame player, IGetPointsPerGame bot, string gameStatus, bool PlayerWon)
        {
            if (whyFinishGame == Constants.ROUNDS_NUM_ENDED)
            {
                if (player.GetPointsPerGame() > bot.GetPointsPerGame())
                    PlayerWon = true;
               else PlayerWon = false;
                gameStatus += Constants.GAME_OVER;
            }
            else if (whyFinishGame == Constants.PLAYER_DIED)
            {
                PlayerWon = false;
                gameStatus += Constants.GAME_OVER;
            }
            else if (whyFinishGame == Constants.BOT_DIED)
            {
                PlayerWon = true;
                gameStatus += Constants.GAME_OVER;
            }

            if (PlayerWon == true)
            {
                player.GlobalRating += player.GetPointsPerGame();
            }
            else
            {
                player.GlobalRating -= Constants.LOSS_OF_POINTS;
            }
        }
    }
}
