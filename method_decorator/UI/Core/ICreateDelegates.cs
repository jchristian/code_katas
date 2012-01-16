namespace UI.Core
{
    public interface ICreateDelegates
    {
        T Combine<T>(T the_first_method, T the_second_method);
    }
}