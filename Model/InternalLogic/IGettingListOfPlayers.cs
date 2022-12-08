using Model.Players_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaTeam1.Model.InternalLogic
{
    public interface IGettingListOfPlayers
    {
        public List<Player> AllPlayers { get; init; }

    }
}
