using System.Collections.Generic;

namespace Data.Models.Entities
{
    public interface IAttack : IEntity
    {
        Damage Attack(IDestructible target, Damage payload);
        IEnumerable<Damage> Attack(IEnumerable<IDestructible> targets, Damage payload);
    }
}