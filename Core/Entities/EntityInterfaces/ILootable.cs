namespace Core.Entities
{
    interface ILootable
    {
        Loot Yield(IActor actor);
    }
}
