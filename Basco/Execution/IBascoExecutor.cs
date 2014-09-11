namespace Basco.Execution
{
    using System;
    using Basco.Log;

    public interface IBascoExecutor<in TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        IBascoLogger ActiveLogger { get; }

        bool AlwaysStartWithInitialState { set; }

        void Initialize(Type startStateType);

        void Start();

        void Stop();

        void ChangeState(TTransitionTrigger trigger);
    }
}