namespace Basco.Execution
{
    using System;

    public interface IBascoExecutor<in TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        void Initialize(Type startStateType);

        void Start(); 

        void Stop();

        void ChangeState(TTransitionTrigger trigger);
    }
}