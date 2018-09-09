using Data.Models.Entities;
using Data.Models.Entities.Humans;
using System.Threading.Tasks;

namespace Data.DataProviders.Players
{
    public interface IPlayerDataProvider
    {
        Task<Player> Get(Id playerId, string connectionId);
        Task<Player> Add(Player player);
        Task Remove(Player player);
    }
}
