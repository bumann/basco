namespace Basco.Execution
{
    using System;

    public interface IBascoStatesProvider<in TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        IState Retrieve(Type stateType);
        
        IState RetrieveNext(IState state, TTransitionTrigger trigger);
    }
}