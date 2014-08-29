namespace Basco.Log
{
    using System;

    public static class StateLoggingExtensions
    {
        public static string LogName<TTransitionTrigger>(this IState state)
            where TTransitionTrigger : IComparable
        {
            var composite = state as IBascoCompositeState<TTransitionTrigger>;
            return composite != null ? string.Format("Composite[{0}]", composite.BaseStateType.Name) : state.GetType().Name;
        }
    }
}