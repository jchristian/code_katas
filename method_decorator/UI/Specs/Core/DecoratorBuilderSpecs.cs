using System;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.moq;
using Machine.Specifications;
using UI.Core;

namespace UI.Specs.Core
{
    public class DecoratorBuilderSpecs
    {
        public abstract class concern : Observes<IBuildADecorator<ITest>,
                                            DecoratorBuilder<ITest>> {}

        [Subject(typeof(DecoratorBuilder<string>))]
        public class when_building_the_decorator_with_a_decoration : concern
        {
            Establish c = () =>
            {
                decoration = x => { };
                the_returned_decorator = fake.an<ITest>();
                var the_decorated_method = new Action<ITest>(x => { });
                var the_compiled_method = new Action<ITest>(x => { });
                var generic_class_builder = fake.an<IBuildAClass<ITest>>();
                var method_substitutor = fake.an<ISubstituteAMethod<ITest>>();

                var the_instance = depends.on<ITest>();
                var the_method_to_decorate = depends.on<IWrapAnExpression<Action<ITest>>>();
                var class_builder = depends.on<ICreateAClass>();
                var delegate_factory = depends.on<ICreateDelegates>();

                class_builder.setup(x => x.Create(the_instance)).Return(generic_class_builder);
                generic_class_builder.setup(x => x.Substituting(the_method_to_decorate)).Return(method_substitutor);
                method_substitutor.setup(x => x.With(the_decorated_method)).Return(the_decorator);
                delegate_factory.setup(x => x.Combine(decoration, the_compiled_method)).Return(the_decorated_method);
                the_method_to_decorate.setup(x => x.Compile()).Return(the_compiled_method);
            };

            Because of = () =>
                the_returned_decorator = sut.With(decoration);

            It should_return_an_object_of_the_same_type = () =>
                the_returned_decorator.ShouldEqual(the_decorator);

            static ITest the_decorator;
            static ITest the_returned_decorator;
            static Action<ITest> decoration;
        }

        public interface ITest {}
    }
}
