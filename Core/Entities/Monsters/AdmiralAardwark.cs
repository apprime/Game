
namespace Core.Entites.Monsters
{
    internal class AdmiralAardwark : Monster
    {
        public AdmiralAardwark()
        {
            Id = Entities.Id.FromString("M1234567891234567890"); //Todo, obviously
            HitPoints = new Entities.HitPoints { Current = 5, Total = 5 };
            //Figure out a clever way to generate Id for monster
            Name = "Admiral Aardwark";
            Description = "The first.";
        }
    }
}