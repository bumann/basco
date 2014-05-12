namespace Basco
{
    using System;

    public static class StateExtensions
    {
        public static ITransitionsBuilder<TTransitionTrigger> On<TTransitionTrigger>(this IState<TTransitionTrigger> state, TTransitionTrigger trigger) 
            where TTransitionTrigger : IComparable
        {
            var transitionsBuilder = new TransitionsBuilder<TTransitionTrigger>(state, () => new TransitionPool<TTransitionTrigger>()) { CurrentTrigger = trigger };
            return transitionsBuilder;
        }
    }
}