using developwithpassion.specifications.extensions;
using developwithpassion.specifications.moq;
using Machine.Specifications;
using UI.Core;

namespace UI.Specs.Core
{
    public class ClassBuilderFactorySpecs
    {
        public abstract class concern : Observes<ICreateAClass,
                                            ClassBuilderFactory> {}

        [Subject(typeof (ClassBuilderFactory))]
        public class when_creating_the_builder : concern
        {
            Establish c = () =>
            {
                the_class_builder_from_the_object_factory = fake.an<IBuildAClass<ITest>>();

                var object_factory = depends.@on<ICreateObjects>();

                object_factory.setup(x => x.With(class_to_wrap)).Return(object_factory);
                object_factory.setup(x => x.GetInstance<IBuildAClass<ITest>>()).Return(the_class_builder_from_the_object_factory);
            };

            Because of = () =>
                the_returned_class_builder = sut.Create(class_to_wrap);

            It should_return_a_class_builder_using_the_passed_in_object = () =>
                the_returned_class_builder.ShouldEqual(the_class_builder_from_the_object_factory);
                
            static ITest class_to_wrap;
            static IBuildAClass<ITest> the_class_builder_from_the_object_factory;
            static IBuildAClass<ITest> the_returned_class_builder;
        }

        public interface ITest {}
    }
}
