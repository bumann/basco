namespace Basco
{
    using System;

    public interface IBascoInternal<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        IBascoExecutor<TTransitionTrigger> BascoExecutor { get; }
    }
}