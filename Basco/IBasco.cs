namespace Basco
{
    using System;

    public interface IBasco<TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        bool IsInitialized { get; }

        bool IsRunning { get; }

        IBascoExecutor<TTransitionTrigger> BascoExecutor { get; }

        void Initialize();

        void Start<TState>() where TState : class, IState;

        void Stop();

        void Trigger(TTransitionTrigger trigger);

        IState RetrieveState<TState>() where TState : class, IState;
    }
}