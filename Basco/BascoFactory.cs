﻿namespace Basco
{
    using System;
    using Basco.Configuration;
    using Basco.Execution;
    using Scyano;

    /// <summary>
    /// Factory to create IBasco if no dependency injection (like Ninject, ...) is used.
    /// </summary>
    public class BascoFactory
    {
        public static IBascoInternal<TTrigger> Create<TTrigger>(
            IBascoConfigurator<TTrigger> bascoConfigurator = null)
            where TTrigger : IComparable
        {
            var stateCache = new BascoStateCache<TTrigger>();
            var transitionCache = new BascoTransitionCache<TTrigger>();
            var stateProvider = new BascoStatesProvider<TTrigger>(stateCache, transitionCache);
            var enterExecutor = new BascoStateEnterExecutor();
            var exitExecutor = new BascoStateExitExecutor();
            var bascoExecutor = new BascoExecutor<TTrigger>(stateProvider, enterExecutor, exitExecutor);
            return new Basco<TTrigger>(ScyanoFactory.Create(), stateCache, transitionCache, bascoExecutor, bascoConfigurator);
        }
    }
}