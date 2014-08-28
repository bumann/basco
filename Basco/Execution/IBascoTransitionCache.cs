namespace Basco.Execution
{
    using System;
    using System.Collections.Generic;
    using Basco.Configuration;

    public interface IBascoTransitionCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        Dictionary<Type, IStateTransitions<TTransitionTrigger>> TransitionPool { get; }

        void Add(Type type, StateTransitions<TTransitionTrigger> stateTransitions);

        Type RetrieveNextStateType(IState state, TTransitionTrigger trigger);
    }
}