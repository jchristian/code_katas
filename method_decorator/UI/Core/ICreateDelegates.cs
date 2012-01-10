namespace UI.Core
{
    public interface ICreateDelegates
    {
        T Combine<T>(T first_method, T second_method);
    }
}