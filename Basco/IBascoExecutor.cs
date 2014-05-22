namespace Basco
{
    using System;

    public interface IBascoExecutor<TTransitionTrigger> 
        where TTransitionTrigger : IComparable
    {
        event EventHandler StateChanged;

        IState<TTransitionTrigger> CurrentState { get; }

        void Start(IState<TTransitionTrigger> initialState);

        void Stop();

        void ChangeState(TTransitionTrigger trigger);
    }
}