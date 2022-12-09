using Model.Players_logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaTeam1.Model.InternalLogic
{
    public interface IGettingListOfPlayers
    {
        public ObservableCollection<Player> AllPlayers { get; init; }

    }
}
