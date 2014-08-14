namespace Basco.Configuration
{
    using System;

    public interface ITransitionConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        ITransitionConfigurator<TTransitionTrigger> WithSubStates(Action<IBascoInternal<TTransitionTrigger>> bascoConfigurator);

        ITransitionConfigurator<TTransitionTrigger> When(TTransitionTrigger trigger);

        ITransitionConfigurator<TTransitionTrigger> Goto<TTargetState>()
            where TTargetState : class;
    }
}