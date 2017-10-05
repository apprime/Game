namespace Data.Models.Entities.EntityInterfaces
{
    public interface IAttack : IPositioned
    {
        int Damage { get; }
        string AttackName { get; }
        DamageType DamageType { get; }
    }
}