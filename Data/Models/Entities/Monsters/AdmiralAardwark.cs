namespace Data.Models.Entities.Monsters
{
    public class AdmiralAardwark : Monster
    {
        public AdmiralAardwark()
        {
            Id = Id.FromString("M001001001001123"); //Todo, obviously
            HitPoints = new HitPoints { Current = 5, Total = 5 };
            Name = "Admiral Aardwark";
            Description = "The first";
            ImageUrl = "m/admiralaardwark.png";
        }
    }
}