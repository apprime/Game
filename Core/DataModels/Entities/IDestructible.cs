namespace Core.Entities
{
    public interface IDestructible : IEntity
    {
        HitPoints HitPoints { get; }
        Damage Mitigate(IAttack attacker, Damage payload);
    }
}