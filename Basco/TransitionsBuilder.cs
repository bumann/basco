namespace Basco
{
    using System;

    internal class TransitionsBuilder<TTransitionTrigger> : ITransitionsBuilder<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly ITransitionPool<TTransitionTrigger> transitionPool;

        public TransitionsBuilder(IState<TTransitionTrigger> state, Func<ITransitionPool<TTransitionTrigger>> transitionPoolFactory)
        {
            this.State = state;
            this.transitionPool = transitionPoolFactory();
        }

        public IState<TTransitionTrigger> State { get; private set; }

        public TTransitionTrigger CurrentTrigger { get; set; }

        public void AddTransition()
        {
            this.SetTransition(this.State);
        }

        public void AddTransition(IState<TTransitionTrigger> toState)
        {
            this.SetTransition(toState);
        }

        private void SetTransition(IState<TTransitionTrigger> toState)
        {
            this.transitionPool.Add(this.CurrentTrigger, toState);
            this.State.Transitions = this.transitionPool;
        }
    }
}