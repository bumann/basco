namespace Basco
{
    using System;

    public interface IBasco<TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        bool IsInitialized { get; }

        bool IsRunning { get; }

        void AddTransitionCache(Type type, StateTransitions<TTransitionTrigger> stateTransitions);

        void InitializeWithStartState<TState>()
            where TState : class, IState;

        void Start();

        void Stop();

        void Trigger(TTransitionTrigger trigger);
    }
}