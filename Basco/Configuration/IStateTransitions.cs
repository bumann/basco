namespace Basco.Configuration
{
    using System;
    using System.Collections.Generic;

    public interface IStateTransitions<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        Dictionary<TTransitionTrigger, Type> Transitions { get; }

        void Add(TTransitionTrigger currentTrigger, Type destinationStateType);
    }
}