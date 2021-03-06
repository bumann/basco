﻿namespace Basco
{
    using System;
    using Basco.Configuration;
    using Basco.Execution;
    using Basco.Log;
    using Scyano;

    /// <summary>
    /// Factory to create IBasco if no dependency injection (like Ninject, ...) is used.
    /// </summary>
    public class BascoFactory
    {
        public static IBascoInternal<TTrigger> Create<TTrigger>(IBascoLogger logger = null)
            where TTrigger : IComparable
        {
            return Create<TTrigger>(null, logger);
        }

        public static IBascoInternal<TTrigger> Create<TTrigger>(IBascoConfigurator<TTrigger> bascoConfigurator = null)
            where TTrigger : IComparable
        {
            return Create(bascoConfigurator, null);
        }

        private static IBascoInternal<TTrigger> Create<TTrigger>(
            IBascoConfigurator<TTrigger> bascoConfigurator,
            IBascoLogger logger)
            where TTrigger : IComparable
        {
            var stateCache = new BascoStateCache<TTrigger>();
            var transitionCache = new BascoTransitionCache<TTrigger>();
            var stateProvider = new BascoStatesProvider<TTrigger>(stateCache, transitionCache);
            var enterExecutor = new BascoStateEnterExecutor();
            var exitExecutor = new BascoStateExitExecutor();
            var loggerProvider = new BascoLoggerProvider(logger);
            var bascoExecutor = new BascoExecutor<TTrigger>(loggerProvider, stateProvider, enterExecutor, exitExecutor);
            return new Basco<TTrigger>(ScyanoFactory.Create<TTrigger>(), stateCache, transitionCache, bascoExecutor, bascoConfigurator);
        }
    }
}