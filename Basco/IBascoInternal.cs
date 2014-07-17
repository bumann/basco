namespace Basco
{
    using System;
    using Basco.Execution;

    public interface IBascoInternal<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        IBascoTransitionCache<TTransitionTrigger> TransitionCache { get; }

        IBascoExecutor<TTransitionTrigger> BascoExecutor { get; }
    }
}