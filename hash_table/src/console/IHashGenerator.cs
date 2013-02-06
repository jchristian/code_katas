namespace console
{
    public interface IHashGenerator<T>
    {
        int GetHash(T key);
    }
}