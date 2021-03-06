﻿namespace Basco.Configuration
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
        public static IBascoInternal<TTransitionTrigger> For<TState, TTransitionTrigger>(this IBascoInternal<TTransitionTrigger> basco, Action<IStateConfigurator<TTransitionTrigger>> configurator)
            where TState : class, IState
            where TTransitionTrigger : IComparable
        {
            var transitionConfigurator = new StateConfigurator<TTransitionTrigger>(basco);
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
        public static IBascoInternal<TTransitionTrigger> ForComposite<TState, TTransitionTrigger>(this IBascoInternal<TTransitionTrigger> basco, Action<ICompositeStateConfigurator<TTransitionTrigger>> configurator)
            where TState : class, IState
            where TTransitionTrigger : IComparable
        {
            var transitionConfigurator = new CompositeStateConfigurator<TTransitionTrigger>(basco);
            transitionConfigurator.ForState<TState>();
            configurator(transitionConfigurator);
            return basco;
        }

        /// <summary>
        /// Option that forces the state machine to start always in its initial state.
        /// Last state when state machine was stopped, is ignored.
        /// Use this option only, if you want to clear "memory" of the state machine.
        /// </summary>
        /// <returns>The hierarchical state machine for fluent configuration.</returns>
        public static IBascoInternal<TTransitionTrigger> AlwaysStartInInitialState<TTransitionTrigger>(this IBascoInternal<TTransitionTrigger> basco)
            where TTransitionTrigger : IComparable
        {
            basco.BascoExecutor.AlwaysStartWithInitialState = true;
            return basco;
        }
    }
}