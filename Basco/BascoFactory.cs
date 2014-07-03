namespace Basco
{
    using System;
    using Scyano;

    /// <summary>
    /// Factory to create IBasco if no dependency injection (like Ninject, ...) is used.
    /// </summary>
    public class BascoFactory
    {
        public static IBasco<TTrigger> Create<TTrigger>(IBascoStatesFactory statesFactory, IBascoConfigurator<TTrigger> bascoConfigurator)
            where TTrigger : IComparable
        {
            var cache = new BascoStateCache(statesFactory);
            return new Basco<TTrigger>(ScyanoFactory.Create(), bascoConfigurator, new BascoExecutor<TTrigger>(cache));
        }
    }
}