using Modlel.Cards;
using System.Text.Json.Serialization;

namespace Model.Cards
{
    public class ImprovesPowerSpell: ICard
    {
        public int Power { get; set; }
        [JsonIgnore]
        public int Health { get; set; }
        public string Name { get; init; }
        
        [JsonConstructor]
        public ImprovesPowerSpell(string Name, int Power)
        {
            this.Name = Name;
            this.Power = Power;
            Health = 0;
        }
    }

    public class ImprovesProtectionSpell : ICard
    {
        public string Name { get; init; }
        [JsonIgnore]
        public int Health { get; set; }
        public int Power { get; set; }

        [JsonConstructor]
        public ImprovesProtectionSpell(string Name, int Power)
        {
            this.Name = Name;
            this.Power = Power;
            Health = 0;
        }
    }
    public class HealsPlayerSpell : ICard
    {
        public string Name { get; init; }
        [JsonIgnore]
        public int Health { get; set; }
        public int Power { get; set; }
        [JsonConstructor]
        public HealsPlayerSpell(string Name, int Power)
        {
            this.Name = Name;
            this.Power = Power;
            Health = 0;
        }
    }
}
