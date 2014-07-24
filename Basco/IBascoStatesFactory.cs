namespace Basco
{
    using System;
    using System.Collections.Generic;

    public interface IBascoStatesFactory<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        IEnumerable<IState> CreateStates(IBasco<TTransitionTrigger> basco);
    }
}