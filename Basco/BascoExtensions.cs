namespace Basco
{
    using System;

    public static class BascoExtensions
    {
        public static IStateTransitionsBuilder<TTransitionTrigger> In<TState, TTransitionTrigger>(this IBasco<TTransitionTrigger> basco)
            where TTransitionTrigger : IComparable
            where TState : class, IState
        {
            var stateTransitions = new StateTransitions<TTransitionTrigger>();
            basco.BascoExecutor.AddStateTransitions<TState>(stateTransitions);
            var transitionsBuilder = new StateTransitionsBuilder<TTransitionTrigger>(typeof(TState), stateTransitions);
            return transitionsBuilder;
        }
    }
}