using Modlel.Cards;

namespace Model.Cards
{
    public class ImprovesPowerSpell: ICard
    {
        public int Power { get; set; }
        public string Name { get; init; }

        public ImprovesPowerSpell(string name, int power)
        {
            this.Name = name;
            this.Power = power;
        }
    }

    public class ImprovesProtectionSpell : ICard
    {
        public string Name { get; init; }
        public int Power { get; set; }
        public ImprovesProtectionSpell(string name, int power)
        {
            this.Name = name;
            this.Power = power;
        }
    }
    public class HealsPlayerSpell : ICard
    {
        public string Name { get; init; }
        public int Power { get; set; }
        public HealsPlayerSpell(string name, int power)
        {
            this.Name = name;
            this.Power = power;
        }
    }


}
