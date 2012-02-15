namespace ui
{
    public interface IBuildFunctions
    {
        IBuildFunctions Constant(decimal value);
        IBuildFunctions Variable(string name);
        IBuildFunctions Add();
        IBuildFunctions Subtract();
        IBuildFunctions Multiply();
        IBuildFunctions Divide();
    }
}