namespace Basco.Execution
{
    using System;

    public interface IBascoTransitionCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        void Add(Type type, StateTransitions<TTransitionTrigger> stateTransitions);

        Type RetrieveNextStateType(IState state, TTransitionTrigger trigger);
    }
}