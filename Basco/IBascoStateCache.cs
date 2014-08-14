namespace Basco
{
    using System;
    using System.Collections.Generic;

    public interface IBascoStateCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        void Initialize(IBasco<TTransitionTrigger> basco, IEnumerable<IState> states);

        void AddComposite(IBascoCompositeState<TTransitionTrigger> state);

        IState RetrieveState(Type stateType);

        IState RetrieveBaseState(Type stateType);

        IEnumerable<IState> RetrieveStates(IEnumerable<Type> stateTypes);
    }
}