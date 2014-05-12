namespace Basco
{
    using System;
    using System.Collections.Generic;

    public class TransitionPool<TTransitionTrigger> : ITransitionPool<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        public TransitionPool()
        {
            this.Transitions = new Dictionary<TTransitionTrigger, IState<TTransitionTrigger>>();
        }

        public Dictionary<TTransitionTrigger, IState<TTransitionTrigger>> Transitions { get; private set; }

        public void Add(TTransitionTrigger currentTrigger, IState<TTransitionTrigger> toState)
        {
            this.Transitions.Add(currentTrigger, toState);
        }
    }
}