namespace Data.DataProviders.Locations.Interfaces
{
    public interface IKnowParent<TParent>
    {
        TParent GetParent(byte id);
    }
}
