namespace Basco.NinjectExtensions
{
    using System;
    using Ninject.Extensions.NamedScope;
    using Ninject.Syntax;

    public static class BindingRootExtensions
    {
        public static IBindingRoot BindBasco(this IBindingRoot syntax, Action<IBascoConfigurator> configurator)
        {
            var bascoConfigurator = new BascoConfigurator(syntax);
            configurator(bascoConfigurator);

            if (string.IsNullOrEmpty(bascoConfigurator.ScopeName))
            {
                syntax.Bind(typeof(IBasco<>)).To(typeof(Basco<>));
                syntax.Bind<IBascoFactory>().To<BascoFactory>();
                syntax.Bind<IBascoStateCache>().To<BascoStateCache>()
                    .OnActivation<IBascoStateCache>(x => x.Initialize());
                syntax.Bind(typeof(IBascoExecutor<>)).To(typeof(BascoExecutor<>));
            }
            else
            {
                syntax.Bind(typeof(IBasco<>)).To(typeof(Basco<>))
                    .InNamedScope(bascoConfigurator.ScopeName);
                syntax.Bind<IBascoFactory>().To<BascoFactory>()
                    .InNamedScope(bascoConfigurator.ScopeName);
                syntax.Bind<IBascoStateCache>().To<BascoStateCache>()
                    .InNamedScope(bascoConfigurator.ScopeName)
                    .OnActivation<IBascoStateCache>(x => x.Initialize());
                    
                syntax.Bind(typeof(IBascoExecutor<>)).To(typeof(BascoExecutor<>))
                    .InNamedScope(bascoConfigurator.ScopeName);
            }

            return syntax;
        }
    }
}
