using System;
using developwithpassion.specifications.moq;
using Machine.Specifications;
using UI.Core;

namespace UI.Specs.Core
{
    public class MethodSubstitutionClassBuilderSpecs
    {
        public abstract class concern : Observes<ISubstituteAMethod<ITest>,
                                            MethodSubstitutionClassBuilder<ITest>> {}

        [Subject(typeof (MethodSubstitutionClassBuilder<ITest>))]
        public class when_scenario : concern
        {
            Establish c = () =>
            {
                
            };

            Because of = () =>
                sut.With(the_method_to_substitute_with);

            It should_build_a_class_that_does_shit = () =>
            { };

            static Action<ITest> the_method_to_substitute_with;
        }

        public interface ITest {}
    }

    public class MethodSubstitutionClassBuilder<ClassType> : ISubstituteAMethod<ClassType>
    {
        public ClassType With(Action<ClassType> the_substitution)
        {
            throw new NotImplementedException();
        }
    }
}
