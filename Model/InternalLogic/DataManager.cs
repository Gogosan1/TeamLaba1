using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Model.Players_logic;
using Modlel.Cards;
using System.IO;
using LabaTeam1.Model.InternalLogic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows;

namespace Model.InternalLogic
{
    internal class DataManager //: IGettingListOfPlayers
    {
        // производить десериалиацию в конструкторе

        public DataManager()
        {
            AllCards = new List<ICard>();
            AllPlayers = new List<Player>();
            AllPlayers = DeserealizationPlayers();

        }
       
        public List<ICard> AllCards { get; init; } 
        public List<Player> AllPlayers { get; init; }
        bool isNewPlayer = true;
        
        string playersFileName = "players.json";
        //string cardsFileName = "Files/Game_cards.json";
   
        
        public void SavePlayerData(Player player)
        {
            foreach (Player p in AllPlayers)
            {
                if (p.NickName == player.NickName)
                {
                    isNewPlayer = false;
                    p.GlobalRating = player.GlobalRating;
                }
            }

            if (isNewPlayer == true)
                AllPlayers.Add(player);
            JsonSerialize();
        }

        private void JsonSerialize()
        {
            string jsonString = JsonSerializer.Serialize<List<Player>>(AllPlayers);
            File.WriteAllText(playersFileName, jsonString);
        }


        private List<Player> DeserealizationPlayers()
        {
            // если пустой файл чета придумать
            string jsonString = File.ReadAllText(playersFileName);
            return JsonSerializer.Deserialize<List<Player>>(jsonString)!;
        }


    }


}
