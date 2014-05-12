namespace Basco
{
    using System;

    public interface IState<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        ITransitionPool<TTransitionTrigger> Transitions { get; set; }

        void Execute();
    }
}