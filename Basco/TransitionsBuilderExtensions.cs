namespace Basco
{
    using System;

    public static class TransitionsBuilderExtensions
    {
        public static ITransitionsBuilder<TTransitionTrigger> On<TTransitionTrigger>(this ITransitionsBuilder<TTransitionTrigger> builder, TTransitionTrigger trigger)
            where TTransitionTrigger : IComparable
        {
            builder.CurrentTrigger = trigger;
            return builder;
        }

        public static ITransitionsBuilder<TTransitionTrigger> Goto<TTransitionTrigger>(this ITransitionsBuilder<TTransitionTrigger> builder, IState<TTransitionTrigger> newState)
            where TTransitionTrigger : IComparable
        {
            builder.AddTransition(newState);
            return builder;
        }

        public static ITransitionsBuilder<TTransitionTrigger> ReEnter<TTransitionTrigger>(this ITransitionsBuilder<TTransitionTrigger> builder)
            where TTransitionTrigger : IComparable
        {
            // TODO: implement
            return Stay(builder);
        }

        public static ITransitionsBuilder<TTransitionTrigger> Stay<TTransitionTrigger>(this ITransitionsBuilder<TTransitionTrigger> builder)
            where TTransitionTrigger : IComparable
        {
            builder.AddTransition();
            return builder;
        }
    }
}