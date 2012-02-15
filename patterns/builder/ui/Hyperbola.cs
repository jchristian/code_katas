namespace ui
{
    public class Hyperbola
    {
        IBuildFunctions _functionBuilder;

        public Hyperbola(IBuildFunctions functionBuilder)
        {
            _functionBuilder = functionBuilder;
        }

        public void Construct()
        {
            _functionBuilder
                .Constant(1).Variable("x").Divide();
        }
    }
}