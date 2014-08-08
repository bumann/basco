namespace Basco.Execution
{
    using System;

    public interface IBascoExecutor<in TTransitionTrigger> : IBascoState
        where TTransitionTrigger : IComparable
    {
        void Initialize(IState startState);

        void Start(); 

        void Stop();

        void ChangeState(TTransitionTrigger trigger);
    }
}