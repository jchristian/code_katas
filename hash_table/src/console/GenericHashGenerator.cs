namespace console
{
    public class GenericHashGenerator<T> : IHashGenerator<T>
    {
        public int GetHash(T key)
        {
            return key.GetHashCode();
        }
    }
}