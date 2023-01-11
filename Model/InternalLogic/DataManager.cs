using System.Collections.Generic;
using System.Text.Json;
using Model.Players_logic;
using Modlel.Cards;
using System.IO;
using Model.Cards;

namespace Model.InternalLogic
{
    internal class DataManager 
    {    
        public DataManager()
        {
            AllCards = new List<ICard>();
            AllPlayers = new List<Player>();
            DeserealizePlayers();
            DeserializeCards();
        }
       
        public List<ICard> AllCards { get; init; } 
        public List<Player> AllPlayers { get; init; }
                
        public void AddPlayer(Player player)
        {
            AllPlayers.Add(player);
            JsonSerializePlayers();
        }
              
        public void JsonSerializePlayers()
        {
            string jsonString = JsonSerializer.Serialize<List<Player>>(AllPlayers);
            File.WriteAllText("Files/players.json", jsonString);
        }

        private void DeserealizePlayers()
        {
            string jsonString = File.ReadAllText("Files/players.json");
            var AllPlayersArray = JsonSerializer.Deserialize<List<Player>>(jsonString)!;
            foreach (var player in AllPlayersArray)
                AllPlayers.Add(player);
        }
        private void DeserializeCards()
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
