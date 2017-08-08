namespace Core
{
    public class Maybe<T>
    {
        public Maybe(T item)
        {
            if (item != null) Value = item;
        }

        public T Value { get; }

        public bool IsSet()
        {
            return Value != null;
        }

        public static implicit operator Maybe<T>(T item)
        {
            return new Maybe<T>(item);
        }
    }
}