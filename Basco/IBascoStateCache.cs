namespace Basco
{
    using System;
    using System.Collections.Generic;

    public interface IBascoStateCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        void Initialize(IBasco<TTransitionTrigger> basco, IEnumerable<IState> states);
            
        IState RetrieveState(Type stateType);

        IEnumerable<IState> RetrieveStates(IEnumerable<Type> stateTypes);
    }
}