namespace Basco
{
    using System;

    public static class StateTransitionsExtensions
    {
        public static Type GetNextStateType<TTransitionTrigger>(this IStateTransitions<TTransitionTrigger> stateTransitions, TTransitionTrigger transitionTrigger)
            where TTransitionTrigger : IComparable
        {
            Type nextStateType;
            stateTransitions.Transitions.TryGetValue(transitionTrigger, out nextStateType);
            return nextStateType;
        }
    }
}