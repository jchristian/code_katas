using System;

namespace UI.Core
{
    public class ReflectionClassBuilder<ClassType> : IBuildAClass<ClassType>
    {
        ICreateObjects object_factory;

        public ReflectionClassBuilder(ICreateObjects object_factory)
        {
            this.object_factory = object_factory;
        }

        public ISubstituteAMethod<ClassType> Substituting(IWrapAnExpression<Action<ClassType>> the_method_to_decorate)
        {
            //var assembly_name = new AssemblyName("MethodDecoration.Dynamic");
            //var assembly_builder = AppDomain.CurrentDomain.DefineDynamicAssembly(assembly_name, AssemblyBuilderAccess.Run);
            //var module_builder = assembly_builder.DefineDynamicModule("DynamicMethods.dll");
            //var type_builder = module_builder.DefineType(typeof(ClassType).Name + Guid.NewGuid(), TypeAttributes.Class, null, new[] { typeof(ClassType) });
            //var method_builder = type_builder.DefineMethod(the_method_to_decorate.Method.Name, MethodAttributes.Public);
            
            //var il_generator = method_builder.GetILGenerator();
            //IL to call the appropriate method...
            //il_generator.Emit(OpCodes.Calli, the_method_to_decorate.Method);

            //Activator.CreateInstance(type_builder);

            return object_factory.With(the_method_to_decorate).GetInstance<ISubstituteAMethod<ClassType>>();
        }
    }
}