using System.Collections.Generic;

namespace Data.DataProviders.Locations.Interfaces
{
    public interface IKnowChildren<TChild>
    {
        IEnumerable<TChild> GetChildren(byte id);
    }
}
