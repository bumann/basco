namespace Basco
{
    using System;

    public interface IBasco<TTransitionTrigger> : IBascoState<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        bool IsRunning { get; }

        void Start<TState>() where TState : IState<TTransitionTrigger>;

        void Stop();

        IState<TTransitionTrigger> RetrieveState<TState>() where TState : IState<TTransitionTrigger>;

        void Trigger(TTransitionTrigger trigger);
    }
}