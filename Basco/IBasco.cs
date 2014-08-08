namespace Basco
{
    using System;
    using System.Collections.Generic;

    public interface IBasco<TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        bool IsInitialized { get; }

        bool IsRunning { get; }

        void AddTransitionCache(Type type, StateTransitions<TTransitionTrigger> stateTransitions);

        void Initialize(IEnumerable<IState> states, IState startState);

        void Start();

        void Stop();

        void Trigger(TTransitionTrigger trigger);
    }
}