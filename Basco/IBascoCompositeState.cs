namespace Basco
{
    using System;

    public interface IBascoCompositeState<TTransitionTrigger> : IStateEnter, IStateExit
        where TTransitionTrigger : IComparable
    {
        Type BaseStateType { get; }

        IBasco<TTransitionTrigger> Basco { get; }

        void Initialize(IBasco<TTransitionTrigger> subBasco, IState baseState);
    }
}