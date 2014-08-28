namespace Basco.Configuration
{
    using System;

    public interface ICompositeStateConfigurator<TTransitionTrigger> : IStateConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        /// <summary>
        /// Configures a composite state.
        /// </summary>
        /// <param name="bascoConfigurator">Configurator for the state machine.</param>
        /// <returns>The transition configurator to continue fluent configuration.</returns>
        ICompositeStateConfigurator<TTransitionTrigger> WithSubStates(Action<IBascoInternal<TTransitionTrigger>> bascoConfigurator);
    }
}