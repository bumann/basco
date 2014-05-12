namespace Basco
{
    using System;
    using System.Collections.Generic;

    public interface ITransitionPool<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        Dictionary<TTransitionTrigger, IState<TTransitionTrigger>> Transitions { get; }

        void Add(TTransitionTrigger currentTrigger, IState<TTransitionTrigger> toState);
    }
}