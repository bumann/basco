namespace Basco
{
    using System;

    public interface ITransitionsBuilder<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        IState<TTransitionTrigger> State { get; }

        TTransitionTrigger CurrentTrigger { get; set; }

        void AddTransition();

        void AddTransition(IState<TTransitionTrigger> newState);
    }
}