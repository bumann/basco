namespace Basco.Configuration
{
    using System;

    public static class BascoExtensions
    {
        public static IBasco<TTransitionTrigger> In<TState, TTransitionTrigger>(this IBasco<TTransitionTrigger> basco, Action<ITransitionConfigurator<TTransitionTrigger>> configurator)
            where TState : class, IState
            where TTransitionTrigger : IComparable
        {
            var transitionConfigurator = new TransitionConfigurator<TTransitionTrigger>(basco);
            transitionConfigurator.ForState<TState>();
            configurator(transitionConfigurator);
            return basco;
        }
    }
}