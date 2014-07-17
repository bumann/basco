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
            IBascoConfigurator<TTrigger> bascoConfigurator)
            where TTrigger : IComparable
        {
            var stateCache = new BascoStateCache(statesFactory);
            var transitionCache = new BascoTransitionCache<TTrigger>();
            var stateProvider = new BascoNextStateProvider<TTrigger>(stateCache, transitionCache);
            var enterExecutor = new BascoStateEnterExecutor();
            var exitExecutor = new BascoStateExitExecutor();
            var bascoExecutor = new BascoExecutor<TTrigger>(stateCache, transitionCache, stateProvider, enterExecutor, exitExecutor);
            return new Basco<TTrigger>(ScyanoFactory.Create(), transitionCache, bascoConfigurator, bascoExecutor);
        }
    }
}