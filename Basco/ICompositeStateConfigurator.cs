namespace Basco
{
    using System;

    public interface ICompositeStateConfigurator
    {
        ICompositeStateConfigurator WithSubState<TState, TTransitionTrigger>(Action<IStateTransitionsBuilder<TTransitionTrigger>> builder)
            where TTransitionTrigger : IComparable
            where TState : class, IState;
    }
}