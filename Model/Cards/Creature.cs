

using System.Text.Json.Serialization;

namespace Modlel.Cards
{
    public class Creature : ICard
    {
        public string Name { get; init; }
        public int Health { get; set; }
        public int Power { get; set; }

        public Creature()
        {
            Name = "";
            Health = 0;
            Power = 0;
        }
        [JsonConstructor]
        public Creature(string Name, int Health, int Power)
        {
            this.Name = Name;
            this.Health = Health;
            this.Power = Power;
        }
    }
}
