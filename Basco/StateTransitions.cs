namespace Basco
{
    using System;
    using System.Collections.Generic;

    public class StateTransitions<TTransitionTrigger> : IStateTransitions<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        public StateTransitions()
        {
            this.Transitions = new Dictionary<TTransitionTrigger, Type>();
        }

        public Dictionary<TTransitionTrigger, Type> Transitions { get; private set; }

        public void Add(TTransitionTrigger currentTrigger, Type destinationStateType)
        {
            this.Transitions.Add(currentTrigger, destinationStateType);
        }
    }
}