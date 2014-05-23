namespace Basco
{
    using System;

    public interface IBascoExecutor<TTransitionTrigger> : IBascoState<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        void Start(IState<TTransitionTrigger> initialState);

        void Stop();

        void ChangeState(TTransitionTrigger trigger);
    }
}