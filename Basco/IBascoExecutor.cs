namespace Basco
{
    using System;

    public interface IBascoExecutor<TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        void AddStateTransitions<TState>(StateTransitions<TTransitionTrigger> stateTransitions)
            where TState : class, IState;

        void Initialize();

        bool Start<TState>() where TState : class, IState;

        void Stop();

        IState RetrieveState<TState>() where TState : class, IState;

        void ChangeState(TTransitionTrigger trigger);
    }
}