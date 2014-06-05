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

        void Trigger(TTransitionTrigger trigger);

        [Obsolete("Use named scope binding and inject the state where needed.")]
        IState RetrieveState<TState>() where TState : class, IState;
    }
}