using Modlel.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LabaTeam1.Model.Cards
{
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
}
