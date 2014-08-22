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

        /// <summary>
        /// Option that forces a composite state to start always with its initial state when entered.
        /// Use this option only, if you want to clear "memory" of the composite state.
        /// For the moment call this AFTER WithSubStates!
        /// </summary>
        /// <returns>The transition configurator to continue fluent configuration.</returns>
        ICompositeStateConfigurator<TTransitionTrigger> OnEnterAlwaysStartWithInitialState();
    }
}