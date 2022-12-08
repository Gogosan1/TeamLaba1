using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Model.Players_logic;
using Modlel.Cards;
using System.IO;

namespace Model.InternalLogic
{
    internal class DataManager
    {
        //TODO: serealisation/desearelesation
        public List<ICard> AllCards { get; init; } = new List<ICard>(); //сюда будем десериализовать список карт из файла
        public List<Player> AllPlayers { get; init; } = new List<Player>();//сюда будем десериализовать список игроков из файла
        bool isNewPlayer = true;
        string playersFileName = "players.json";
        string cardsFileName = "cards.json";
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
        }

        public async Task SerealizationAsync()
        {
            using (FileStream fs = new FileStream(playersFileName, FileMode.OpenOrCreate))
                foreach (Player p in AllPlayers)
                    await JsonSerializer.SerializeAsync<Player>(fs, p);
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
