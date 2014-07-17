namespace Basco
{
    using System;
    using Basco.Execution;

    public interface IBascoInternal<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        IBascoExecutor<TTransitionTrigger> BascoExecutor { get; }
    }
}