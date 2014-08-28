namespace Basco.Configuration
{
    using System;

    public interface IStateTransitionsBuilder<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        Type StateType { get; }

        TTransitionTrigger CurrentTrigger { get; set; }

        void AddTransition<TState>() where TState : class, IState;

        void AddTransition();
    }
}