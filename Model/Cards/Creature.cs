

namespace Modlel.Cards
{
    public class Creature : ICreature
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
        public Creature(string name, int health, int power)
        {
            Name = name;
            Health = health;
            Power = power;
        }
    }
}
