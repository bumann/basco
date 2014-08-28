namespace Basco
{
    using System;
    using Basco.Configuration;
    using Basco.Execution;

    public interface IBascoInternal<TTransitionTrigger> : IBasco<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        IBascoStateCache<TTransitionTrigger> StatesCache { get; }

        IBascoTransitionCache<TTransitionTrigger> TransitionCache { get; }

        IBascoExecutor<TTransitionTrigger> BascoExecutor { get; }

        void AddTransitionCache(Type type, StateTransitions<TTransitionTrigger> stateTransitions);

        void AddHierchary(IBascoInternal<TTransitionTrigger> subBasco);
    }
}