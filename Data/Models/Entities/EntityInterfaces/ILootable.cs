namespace Data.Models.Entities.EntityInterfaces
{
    public interface ILootable
    {
        Loot Yield(IActor actor);
    }
}
