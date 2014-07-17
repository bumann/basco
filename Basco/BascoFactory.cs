namespace Basco
{
    using System;
    using Basco.Execution;
    using Scyano;

    /// <summary>
    /// Factory to create IBasco if no dependency injection (like Ninject, ...) is used.
    /// </summary>
    public class BascoFactory
    {
        public static IBasco<TTrigger> Create<TTrigger>(
            IBascoStatesFactory statesFactory,
            IBascoConfigurator<TTrigger> bascoConfigurator,
            IBascoStateEnterExecutor stateEnterExecutor,
            IBascoStateExitExecutor stateExitExecutor)
            where TTrigger : IComparable
        {
            var stateCache = new BascoStateCache(statesFactory);
            var transitionCache = new BascoTransitionCache<TTrigger>();
            var stateProvider = new BascoNextStateProvider<TTrigger>(stateCache, transitionCache);
            var bascoExecutor = new BascoExecutor<TTrigger>(stateCache, transitionCache, stateProvider, stateEnterExecutor, stateExitExecutor);
            return new Basco<TTrigger>(ScyanoFactory.Create(), transitionCache, bascoConfigurator, bascoExecutor);
        }
    }
}