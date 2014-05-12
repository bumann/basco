namespace Basco
{
    using System;

    public static class TransitionPoolExtensions
    {
        public static IState<TTransitionTrigger> GetNextState<TTransitionTrigger>(this ITransitionPool<TTransitionTrigger> transitionPool, TTransitionTrigger transitionTrigger)
            where TTransitionTrigger : IComparable
        {
            IState<TTransitionTrigger> state;
            transitionPool.Transitions.TryGetValue(transitionTrigger, out state);
            return state;
        }
    }
}