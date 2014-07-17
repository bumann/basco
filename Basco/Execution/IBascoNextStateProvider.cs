namespace Basco.Execution
{
    using System;

    public interface IBascoNextStateProvider<in TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        IState RetrieveNext(IState state, TTransitionTrigger trigger);
    }
}