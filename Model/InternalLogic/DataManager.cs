using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Model.Players_logic;
using Modlel.Cards;
using System.IO;
using LabaTeam1.Model.InternalLogic;
using System.Collections.ObjectModel;

namespace Model.InternalLogic
{
    internal class DataManager : IGettingListOfPlayers
    {
        // производить десериалиацию в конструкторе

        public DataManager()
        {
            AllCards = new List<ICard>();
            AllPlayers = new ObservableCollection<Player>();
            SerealizationAsync();
            DeserealizationAsync();
        }
        //TODO: serealisation/desearelesation
        public List<ICard> AllCards { get; init; } //сюда будем десериализовать список карт из файла
        public ObservableCollection<Player> AllPlayers { get; init; }//сюда будем десериализовать список игроков из файла
        bool isNewPlayer = true;
        string playersFileName = "players.json";
        string cardsFileName = "Files/Game_cards.json";
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
            DeserealizationAsync();
        }

        public async Task SerealizationAsync()
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };

            using (FileStream fs = new FileStream(playersFileName, FileMode.OpenOrCreate))
                foreach (Player p in AllPlayers)
                   await JsonSerializer.SerializeAsync<Player>(fs,p);
        }
        public async Task DeserealizationAsync()
        {
            using (FileStream fs = new FileStream(playersFileName, FileMode.OpenOrCreate))
            {
                Player? p = await JsonSerializer.DeserializeAsync<Player>(fs);
                while (p != null)
                {
                    AllPlayers.Add(p);
                }
            }

            using (FileStream fs = new FileStream(cardsFileName, FileMode.OpenOrCreate))
            {
                ICard? p = await JsonSerializer.DeserializeAsync<ICard>(fs);
                while (p != null)
                {
                    AllCards.Add(p);
                }
            }
        }
    }


}
