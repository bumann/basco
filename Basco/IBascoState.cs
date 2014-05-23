namespace Basco
{
    using System;

    public interface IBascoState<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        event EventHandler StateChanged;

        IState<TTransitionTrigger> CurrentState { get; }
    }
}