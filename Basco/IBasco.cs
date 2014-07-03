namespace Basco
{
    using System;

    public interface IBasco<TTransitionTrigger> : IBascoState, IBascoInternal<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        bool IsInitialized { get; }

        bool IsRunning { get; }

        void InitializeWithStartState<TState>() where TState : class, IState;

        void Start();

        void Stop();

        void Trigger(TTransitionTrigger trigger);
    }
}