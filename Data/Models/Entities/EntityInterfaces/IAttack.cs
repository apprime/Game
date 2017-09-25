using System.Collections.Generic;

namespace Data.Models.Entities.EntityInterfaces
{
    public interface IAttack : IPositioned
    {
        Damage Attack(IDestructible target, Damage payload);
        IEnumerable<Damage> Attack(IEnumerable<IDestructible> targets, Damage payload);
    }
}