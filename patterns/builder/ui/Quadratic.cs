namespace ui
{
    public class Quadratic
    {
        IBuildFunctions _functionBuilder;

        public Quadratic(IBuildFunctions functionBuilder)
        {
            _functionBuilder = functionBuilder;
        }

        public void Construct()
        {
            _functionBuilder
                .Variable("x").Variable("x").Multiply();
        }
    }
}