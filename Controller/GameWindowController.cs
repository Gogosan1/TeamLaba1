using Model.InternalLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Controller
{
    public class GameWindowController
    {
        public GameWindowController()
        {
            game = new Game();
        }

        public string MakeAMove(ICard card)
        {

        }
        
        private Game game;
    }
}
