namespace Basco.Configuration
{
    using System;

    public interface IStateConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        /// <summary>
        /// Configures a composite state.
        /// </summary>
        /// <param name="bascoConfigurator">Configurator for the state machine.</param>
        /// <returns>The transition configurator to continue fluent configuration.</returns>
        IStateConfigurator<TTransitionTrigger> WithSubStates(Action<IBascoInternal<TTransitionTrigger>> bascoConfigurator);

        /// <summary>
        /// Option that forces a composite state to start always with its initial state when entered.
        /// Use this option only, if you want to clear "memory" of the composite state. 
        /// </summary>
        /// <returns>The transition configurator to continue fluent configuration.</returns>
        IStateConfigurator<TTransitionTrigger> OnEnterAlwaysStartWithInitialState();

        /// <summary>
        /// Defines the transition for which a state change occurs.
        /// Use Goto afterwards to define the destination of the transition.
        /// </summary>
        /// <param name="trigger">The enum defining all transitions in the state machine.</param>
        /// <returns>The transition configurator to continue fluent configuration.</returns>
        IStateConfigurator<TTransitionTrigger> When(TTransitionTrigger trigger);

        /// <summary>
        /// Defines the destination state for a transition.
        /// </summary>
        /// <typeparam name="TTargetState">The destination state of the transition.</typeparam>
        /// <returns>The transition configurator to continue fluent configuration.</returns>
        IStateConfigurator<TTransitionTrigger> Goto<TTargetState>()
            where TTargetState : class;
    }
}