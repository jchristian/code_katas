namespace UI.Core
{
    public interface ICreateObjects
    {
        ICreateObjects With<ArgumentType>(ArgumentType classToWrap);
        T GetInstance<T>();
    }
}