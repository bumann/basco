namespace Basco
{
    using System;

    public interface IBascoStateCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        void Initialize(IBasco<TTransitionTrigger> basco);
            
        IState RetrieveState(Type stateType);
    }
}