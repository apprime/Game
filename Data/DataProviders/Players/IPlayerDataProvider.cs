using Data.Models.Entities;
using Data.Models.Entities.Humans;

namespace Data.DataProviders.Players
{
    public interface IPlayerDataProvider
    {
        Player Get(Id playerId, string connectionId);
        Player Add(Player player);
        void Remove(Player player);
    }
}
