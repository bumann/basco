namespace Basco.NinjectExtensions
{
    using Appccelerate.AsyncModule;
    using Ninject.Extensions.NamedScope;
    using Ninject.Syntax;

    public static class BindingRootExtensions
    {
        public static IBindingRoot BindBasco(this IBindingRoot syntax)
        {
            syntax.Bind<IModuleController>().To<ModuleController>();

            syntax.Bind(typeof(IBasco<>)).To(typeof(Basco<>));
            //// TODO:
            //// syntax.Bind(typeof(IBascoExecutor<>)).To(typeof(BascoExecutor<>));
            syntax.Bind(typeof(IStateTransitions<>)).To(typeof(StateTransitions<>));

            return syntax;
        }

        public static IBindingRoot InNamedScope(this IBindingRoot syntax, string namedScopeName)
        {
            syntax.Bind(typeof(IBascoExecutor<>)).To(typeof(BascoExecutor<>))
                .InNamedScope(namedScopeName);

            return syntax;
        }

        public static void WithConfigurator<TBascoConfigurator>(this IBindingRoot syntax)
        {
            syntax.Bind(typeof(IBascoConfigurator<>)).To<TBascoConfigurator>();
        }
    }
}
