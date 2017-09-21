namespace Data.Models.Entities
{
    public interface ILootable
    {
        Loot Yield(IActor actor);
    }
}
