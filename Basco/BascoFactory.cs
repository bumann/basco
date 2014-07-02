namespace Basco
{
    using System;

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