using Model.InternalLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Controller
{
    public class StartWindowController
    {
        private Game game;
        public StartWindowController()
        {
            game = new Game();
        }

       public bool IsPlayerByNickExist(string name)
        {
            if (game.IsPlayerExist(name))
                return true;
            else
            {
                game.AddPlayer(name);
                return false;
            }
        }
        
         
            /*
           foreach (var player in game.Players)
                if (player.Name == name)
                    return true;
           return false;*/
        
    }
}
