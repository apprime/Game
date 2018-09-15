using Data.Models.Entities.Humans;
using Data.Models.Nodes;
using Data.Repositories.Nodes;
using System.Threading.Tasks;

namespace Data.Factories
{
    public static class LocationFactory
    {
        public static Location GetOrCreate(Position position, Player player)
        {
            var repo = new SectorRepository(); //TODO: Manage this in another way
            var sector = repo.Get(position);

            var candidate = sector.Get(position, player.Party);
            if (candidate == null)
            {
                return sector.Create(position);
            }
            else
            {
                return candidate;
            }
        }
    }
}
