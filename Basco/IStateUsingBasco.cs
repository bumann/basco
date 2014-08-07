namespace Basco
{
    using System;

    public interface IStateUsingBasco<TTransitionTrigger> : IState
        where TTransitionTrigger : IComparable
    {
        IBasco<TTransitionTrigger> Basco { get; set; }
    }
}