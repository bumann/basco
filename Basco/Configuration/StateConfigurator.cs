namespace Basco.Configuration
{
    using System;

    internal class StateConfigurator<TTransitionTrigger> : IStateConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        protected readonly IBascoInternal<TTransitionTrigger> Basco;
        private readonly StateTransitions<TTransitionTrigger> stateTransitions;
        private TTransitionTrigger nextTrigger;

        public StateConfigurator(IBascoInternal<TTransitionTrigger> basco)
        {
            this.Basco = basco;
            this.stateTransitions = new StateTransitions<TTransitionTrigger>();
        }

        public virtual void ForState<TState>()
            where TState : class, IState
        {
            this.Basco.AddTransitionCache(typeof(TState), this.stateTransitions);
        }

        public IStateConfigurator<TTransitionTrigger> When(TTransitionTrigger trigger)
        {
            this.nextTrigger = trigger;
            return this;
        }

        public IStateConfigurator<TTransitionTrigger> Goto<TTargetState>()
            where TTargetState : class
        {
            this.stateTransitions.Add(this.nextTrigger, typeof(TTargetState));
            return this;
        }
    }
}