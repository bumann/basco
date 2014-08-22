namespace Basco.Configuration
{
    using System;

    public static class BascoExtensions
    {
        /// <summary>
        /// Extension to configure a state of the 'Basco' hierarchical state machine.
        /// </summary>
        /// <typeparam name="TState">The state (IState) that is configured.</typeparam>
        /// <typeparam name="TTransitionTrigger">The enum defining all transitions in the state machine.</typeparam>
        /// <param name="basco">The hierarchical state machine to configure.</param>
        /// <param name="configurator">Action to configure the transitions of the state.</param>
        /// <returns>The hierarchical state machine for fluent configuration.</returns>
        public static IBascoInternal<TTransitionTrigger> For<TState, TTransitionTrigger>(this IBascoInternal<TTransitionTrigger> basco, Action<ITransitionConfigurator<TTransitionTrigger>> configurator)
            where TState : class, IState
            where TTransitionTrigger : IComparable
        {
            var transitionConfigurator = new TransitionConfigurator<TTransitionTrigger>(basco);
            transitionConfigurator.ForState<TState>();
            configurator(transitionConfigurator);
            return basco;
        }

        /// <summary>
        /// Extension to configure a composite state of the 'Basco' hierarchical state machine.
        /// </summary>
        /// <typeparam name="TState">The composite state (IState) that is configured.</typeparam>
        /// <typeparam name="TTransitionTrigger">The enum defining all transitions in the state machine.</typeparam>
        /// <param name="basco">The hierarchical state machine to configure.</param>
        /// <param name="configurator">Action to configure the transitions of the composite state.</param>
        /// <returns>The hierarchical state machine for fluent configuration.</returns>
        public static IBascoInternal<TTransitionTrigger> ForComposite<TState, TTransitionTrigger>(this IBascoInternal<TTransitionTrigger> basco, Action<ITransitionConfigurator<TTransitionTrigger>> configurator)
            where TState : class, IState
            where TTransitionTrigger : IComparable
        {
            var transitionConfigurator = new TransitionConfigurator<TTransitionTrigger>(basco);
            transitionConfigurator.ForCompositeState<TState>();
            configurator(transitionConfigurator);
            return basco;
        }
    }
}