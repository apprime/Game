namespace Data.DataProviders.Locations
{
    public interface IPositionDataProvider<T>
    {
        T Get(string id);
    }
}
