namespace Basco
{
    using System;
    using System.Collections.Generic;

    public interface IBasco<in TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        bool IsInitialized { get; }

        bool IsRunning { get; }

        void Initialize(IEnumerable<IState> states, Type startStateType);

        void Start();

        void Stop();

        void Trigger(TTransitionTrigger trigger);
    }
}