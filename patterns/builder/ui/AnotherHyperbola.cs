namespace ui
{
    public class AnotherHyperbola
    {
        IBuildFunctions _functionBuilder;

        public AnotherHyperbola(IBuildFunctions functionBuilder)
        {
            _functionBuilder = functionBuilder;
        }

        public void Construct()
        {
            _functionBuilder
                .Constant(1).Variable("x").Variable("x")
                .Subtract().Divide();
        }
    }
}