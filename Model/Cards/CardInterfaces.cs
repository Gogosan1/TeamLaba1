

namespace Modlel.Cards
{
    public interface ICard
    {
        string Name { get; init; }
        int Health { get; set; }
        int Power { get; set; }
    }
}
