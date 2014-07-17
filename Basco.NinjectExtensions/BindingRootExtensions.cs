﻿namespace Basco.NinjectExtensions
{
    using System;
    using Basco.Execution;
    using Ninject.Extensions.NamedScope;
    using Ninject.Syntax;

    public static class BindingRootExtensions
    {
        public static IBindingRoot BindBasco(this IBindingRoot syntax, Action<IBascoConfigurator> configurator)
        {
            var bascoConfigurator = new BascoConfigurator(syntax);
            configurator(bascoConfigurator);

            syntax.Bind<IBascoFactory>().To<BascoFactory>();
            syntax.Bind<IBascoStatesFactory>().To<BascoStatesFactory>();

            if (string.IsNullOrEmpty(bascoConfigurator.ScopeName))
            {
                syntax.Bind(typeof(IBasco<>)).To(typeof(Basco<>));
                syntax.Bind<IBascoStateCache>().To<BascoStateCache>();
                syntax.Bind(typeof(IBascoTransitionCache<>)).To(typeof(BascoTransitionCache<>));
                syntax.Bind(typeof(IBascoNextStateProvider<>)).To(typeof(BascoNextStateProvider<>));
                syntax.Bind(typeof(IBascoExecutor<>)).To(typeof(BascoExecutor<>));
                syntax.Bind(typeof(IBascoStateEnterExecutor)).To(typeof(BascoStateEnterExecutor));
                syntax.Bind(typeof(IBascoStateExitExecutor)).To(typeof(BascoStateExitExecutor));
            }
            else
            {
                syntax.Bind(typeof(IBasco<>)).To(typeof(Basco<>))
                    .InNamedScope(bascoConfigurator.ScopeName);
                syntax.Bind<IBascoStateCache>().To<BascoStateCache>()
                    .InNamedScope(bascoConfigurator.ScopeName);
                syntax.Bind(typeof(IBascoTransitionCache<>)).To(typeof(BascoTransitionCache<>))
                    .InNamedScope(bascoConfigurator.ScopeName);
                syntax.Bind(typeof(IBascoNextStateProvider<>)).To(typeof(BascoNextStateProvider<>))
                   .InNamedScope(bascoConfigurator.ScopeName);
                syntax.Bind(typeof(IBascoExecutor<>)).To(typeof(BascoExecutor<>))
                    .InNamedScope(bascoConfigurator.ScopeName);
                syntax.Bind(typeof(IBascoStateEnterExecutor)).To(typeof(BascoStateEnterExecutor))
                    .InNamedScope(bascoConfigurator.ScopeName);
                syntax.Bind(typeof(IBascoStateExitExecutor)).To(typeof(BascoStateExitExecutor))
                    .InNamedScope(bascoConfigurator.ScopeName);
            }

            return syntax;
        }
    }
}
