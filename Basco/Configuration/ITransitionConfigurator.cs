namespace Basco.Configuration
{
    using System;

    public interface ITransitionConfigurator<in TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        ITransitionConfigurator<TTransitionTrigger> When(TTransitionTrigger trigger);

        ITransitionConfigurator<TTransitionTrigger> Goto<TTargetState>()
            where TTargetState : class;
    }
}