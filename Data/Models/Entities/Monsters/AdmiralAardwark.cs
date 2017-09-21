namespace Data.Models.Entities.Monsters
{
    public class AdmiralAardwark : Monster
    {
        public AdmiralAardwark()
        {
            Id = Id.FromString("M1234567891234567890"); //Todo, obviously
            HitPoints = new HitPoints { Current = 5, Total = 5 };
            //Figure out a clever way to generate Id for monster
            Name = "Admiral Aardwark";
            Description = "The first.";
        }
    }
}