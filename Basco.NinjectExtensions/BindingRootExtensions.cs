namespace Basco.NinjectExtensions
{
    using System;
    using Appccelerate.AsyncModule;
    using Ninject.Syntax;

    public static class BindingRootExtensions
    {
        public static IBindingRoot BindBasco(this IBindingRoot syntax)
        {
            syntax.Bind<IModuleController>().To<ModuleController>();
            return syntax;
        }

        public static void ForTriggers<TTrigger>(this IBindingRoot syntax)
            where TTrigger : IComparable
        {
            syntax.Bind<IBasco<TTrigger>>().To<Basco<TTrigger>>();
            syntax.Bind<IBascoExecutor<TTrigger>>().To<BascoExecutor<TTrigger>>();
            syntax.Bind<IStateTransitions<TTrigger>>().To<StateTransitions<TTrigger>>();
        }
    }
}
