using System.Collections.Generic;
using System.Text.Json;
using Model.Players_logic;
using Modlel.Cards;
using System.IO;
using Model.Cards;

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
            DeserializationCards();
        }
       
        public List<ICard> AllCards { get; init; } 
        public List<Player> AllPlayers { get; init; }
        bool isNewPlayer = true;
        
        //string playersFileName = "Files/players.json";
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
            File.WriteAllText("Files/players.json", jsonString);
        }


        private List<Player> DeserealizationPlayers()
        {
            // если пустой файл чета придумать
            string jsonString = File.ReadAllText("Files/players.json");
            return JsonSerializer.Deserialize<List<Player>>(jsonString)!;
        }

        private void DeserializationCards()
        {
            string creaturesFile = File.ReadAllText("Files/Creatures.json");
            var creatures = JsonSerializer.Deserialize<List<Creature>>(creaturesFile)!;
            foreach(Creature creature in creatures)
                AllCards.Add(creature);

            string HealsPlayerSpellFile = File.ReadAllText("Files/HealsPlayerSpell.json");
            var HealsPlayerSpells = JsonSerializer.Deserialize<List<HealsPlayerSpell>>(HealsPlayerSpellFile)!;
            foreach (HealsPlayerSpell spell in HealsPlayerSpells)
                AllCards.Add(spell);

            string ImprovesPowerSpellFile = File.ReadAllText("Files/ImprovesPowerSpell.json");
            var ImprovesPowerSpells = JsonSerializer.Deserialize<List<ImprovesPowerSpell>>(ImprovesPowerSpellFile)!;
            foreach (ImprovesPowerSpell spell1 in ImprovesPowerSpells)
                AllCards.Add(spell1);

            string ImprovesProtectionSpellFile = File.ReadAllText("Files/ImprovesProtectionSpell.json");
            var ImprovesProtectionSpells = JsonSerializer.Deserialize<List<ImprovesProtectionSpell>>(ImprovesProtectionSpellFile)!;
            foreach (ImprovesProtectionSpell spell2 in ImprovesProtectionSpells)
                AllCards.Add(spell2);
        }

    }


}
