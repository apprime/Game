using Data.Models.Entities.EntityInterfaces;

namespace Data.Models.Entities
{
    public interface IDestructible : IPositioned
    {
        HitPoints HitPoints { get; }
        Damage Mitigate(IAttack attacker, Damage payload);
    }
}