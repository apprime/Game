using Data.Models.Entities.EntityInterfaces;

namespace Data.Models.Entities
{
    public class Damage
    {
        public IAttack Actor { get; set; }
        public IDestructible Target { get; set; }

        public string Name { get; set; }
        public int Total { get; set; }
        public int Effective { get; set; }
        public string DamageType { get; set; } //Todo: Create some sort of lookup table for damage types (enum)
        public string AlternativeEffect { get; set; } //Todo: Such things as misses, dodges or parries. Completely not developed.

        public override string ToString()
        {
            //Todo: make this make sense
            return string.Format("{0} did {1} {2} damage to {3}", Actor.Name, Effective, DamageType, Target.Name);
        }
    }

    public enum DamageType
    {
        DAMAGE_TYPE_NOT_SET = 0,
        Physical = 1
    }
}
