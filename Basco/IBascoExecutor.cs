namespace Basco
{
    using System;

    public interface IBascoExecutor<TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        void AddStateTransitions<TState>(StateTransitions<TTransitionTrigger> stateTransitions)
            where TState : class, IState;

        void InitializeWithStartState<TState>() where TState : class, IState;

        void Start(); 

        void Stop();

        void ChangeState(TTransitionTrigger trigger);
    }
}