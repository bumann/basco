namespace Basco.Configuration
{
    using System;

    public interface IStateConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
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