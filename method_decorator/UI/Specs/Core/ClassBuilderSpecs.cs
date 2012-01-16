using System;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.moq;
using Machine.Specifications;
using UI.Core;

namespace UI.Specs.Core
{
    public class ClassBuilderSpecs
    {
        public abstract class concern : Observes<IBuildAClass<ITest>,
                                            ReflectionClassBuilder<ITest>> {}

        [Subject(typeof (ReflectionClassBuilder<ITest>))]
        public class when_substituting_a_method_implementation : concern
        {
            Establish c = () =>
            {
                var object_factory = depends.on<ICreateObjects>();

                object_factory.setup(x => x.With(expression)).Return(object_factory);
                the_method_to_substitute_from_the_factory = fake.an<ISubstituteAMethod<ITest>>();
                object_factory.setup(x => x.GetInstance<ISubstituteAMethod<ITest>>()).Return(the_method_to_substitute_from_the_factory);
            };
            
            Because of = () =>
                the_returned_method_subsitutor = sut.Substituting(expression);

            It should_return_a_method_substitutor_created_with_the_expression = () =>
                the_returned_method_subsitutor.ShouldEqual(the_method_to_substitute_from_the_factory);

            static IWrapAnExpression<Action<ITest>> expression;
            static ISubstituteAMethod<ITest> the_method_to_substitute_from_the_factory;
            static ISubstituteAMethod<ITest> the_returned_method_subsitutor;
        }

        public interface ITest {}
    }
}
