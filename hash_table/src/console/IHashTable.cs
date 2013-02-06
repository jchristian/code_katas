namespace console
{
    public interface IHashTable<TKey, TValue>
    {
        TValue this[TKey index] { get; set; }
    }
}