namespace Basco
{
    using System;

    public static class StateTransitionsBuilderExtensions
    {
        public static IStateTransitionsBuilder<TTransitionTrigger> On<TTransitionTrigger>(this IStateTransitionsBuilder<TTransitionTrigger> builder, TTransitionTrigger trigger)
            where TTransitionTrigger : IComparable
        {
            builder.CurrentTrigger = trigger;
            return builder;
        }

        public static IStateTransitionsBuilder<TTransitionTrigger> Goto<TState, TTransitionTrigger>(this IStateTransitionsBuilder<TTransitionTrigger> builder)
            where TTransitionTrigger : IComparable
            where TState : class, IState
        {
            builder.AddTransition<TState>();
            return builder;
        }

        public static IStateTransitionsBuilder<TTransitionTrigger> ReEnter<TTransitionTrigger>(this IStateTransitionsBuilder<TTransitionTrigger> builder)
            where TTransitionTrigger : IComparable
        {
            // TODO: implement
            return Stay(builder);
        }

        public static IStateTransitionsBuilder<TTransitionTrigger> Stay<TTransitionTrigger>(this IStateTransitionsBuilder<TTransitionTrigger> builder)
            where TTransitionTrigger : IComparable
        {
            builder.AddTransition();
            return builder;
        }
    }
}