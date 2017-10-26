using Data.Models.Entities.EntityInterfaces;
using Data.Repositories.Nodes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Data.Models.Nodes
{
    public class Sector
    {
        [JsonIgnore]
        public List<Location> Locations = new List<Location>();

        public void RemoveLocation(Location location)
        {
            Locations.Remove(location);

            if (!Locations.Any())
            {
                //TODO: Remove itself from Repo?
                //TODO2: Should GlobalLocations also do this?
            }
        }

        public Location Get(Position position, IEnumerable<IEntity> party)
        {
            //TODO: Make this a dictionary with Key Location for lookup.
            var locations = Locations.Where(i => i.Position.Location == position.Location);

            if(!locations.Any())
            {
                return null;
            }

            if (locations.First() is GlobalLocation)
            {
                //Global Locations must always be alone!
                return locations.First();
            }

            //Rule: A party can never create more than one Scene per location. FirstOrDefault is therefor all we need.
            return locations.FirstOrDefault(i => IsPartyInScene(party, i));
        }

        public Location Create(Position pos)
        {
            var repo = new LocationRepository();
            var location = repo.Create(pos);

            if(Locations.Count == 1)
            {
                Locations.Add(location);
            }

            return location;
        }

        private static bool IsPartyInScene(IEnumerable<IEntity> party, Location location)
        {
            return location.Players.Intersect(party).Any();
        }
    }
}
