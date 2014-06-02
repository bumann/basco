namespace Basco
{
    using System;

    public interface IBasco<TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        bool IsRunning { get; }

        IBascoExecutor<TTransitionTrigger> BascoExecutor { get; }

        void Start<TState>() where TState : class, IState;

        void Stop();

        IState RetrieveState<TState>() where TState : class, IState;

        void Trigger(TTransitionTrigger trigger);
    }
}