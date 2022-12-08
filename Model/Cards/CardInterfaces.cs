

namespace Modlel.Cards
{
    public interface ICard
    {
        string Name { get; init; }
        int Power { get; set; }
    }

    public interface ICreature
    {
        int Health { get; set; }
    }
}
