namespace Data.Models.Entities
{
    public interface IDestructible : IEntity
    {
        HitPoints HitPoints { get; }
        Damage Mitigate(IAttack attacker, Damage payload);
    }
}