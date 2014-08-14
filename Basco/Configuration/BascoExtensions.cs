namespace Basco.Configuration
{
    using System;

    public static class BascoExtensions
    {
        public static IBascoInternal<TTransitionTrigger> For<TState, TTransitionTrigger>(this IBascoInternal<TTransitionTrigger> basco, Action<ITransitionConfigurator<TTransitionTrigger>> configurator)
            where TState : class, IState
            where TTransitionTrigger : IComparable
        {
            var transitionConfigurator = new TransitionConfigurator<TTransitionTrigger>(basco);
            transitionConfigurator.ForState<TState>();
            configurator(transitionConfigurator);
            return basco;
        }

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