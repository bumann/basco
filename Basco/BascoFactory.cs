namespace Basco
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
        public static IBasco<TTrigger> Create<TTrigger>(
            IBascoStatesFactory<TTrigger> statesFactory,
            IBascoConfigurator<TTrigger> bascoConfigurator)
            where TTrigger : IComparable
        {
            var stateCache = new BascoStateCache<TTrigger>(statesFactory);
            var transitionCache = new BascoTransitionCache<TTrigger>();
            var stateProvider = new BascoNextStateProvider<TTrigger>(stateCache, transitionCache);
            var enterExecutor = new BascoStateEnterExecutor();
            var exitExecutor = new BascoStateExitExecutor();
            var bascoExecutor = new BascoExecutor<TTrigger>(stateCache, stateProvider, enterExecutor, exitExecutor);
            return new Basco<TTrigger>(ScyanoFactory.Create(), transitionCache, bascoConfigurator, bascoExecutor);
        }
    }
}