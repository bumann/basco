namespace Basco.Configuration
{
    using System;

    internal class TransitionConfigurator<TTransitionTrigger> : ITransitionConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBasco<TTransitionTrigger> basco;
        private readonly StateTransitions<TTransitionTrigger> stateTransitions;
        private TTransitionTrigger nextTrigger;

        public TransitionConfigurator(IBasco<TTransitionTrigger> basco)
        {
            this.basco = basco;
            this.stateTransitions = new StateTransitions<TTransitionTrigger>();
        }

        public void ForState<TState>()
            where TState : class
        {
            this.basco.AddTransitionCache(typeof(TState), this.stateTransitions);
        }

        public ITransitionConfigurator<TTransitionTrigger> When(TTransitionTrigger trigger)
        {
            this.nextTrigger = trigger;
            return this;
        }

        public ITransitionConfigurator<TTransitionTrigger> Goto<TTargetState>()
            where TTargetState : class
        {
            this.stateTransitions.Add(this.nextTrigger, typeof(TTargetState));
            return this;
        }
    }
}