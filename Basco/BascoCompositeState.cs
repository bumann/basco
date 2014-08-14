namespace Basco
{
    using System;

    public class BascoCompositeState<TTransitionTrigger, TState> : IBascoCompositeState<TTransitionTrigger>
        where TTransitionTrigger : IComparable
        where TState : IState
    {
        private IBasco<TTransitionTrigger> basco;
        private IState baseState;

        public BascoCompositeState()
        {
            this.BaseStateType = typeof(TState);
        }

        public Type BaseStateType { get; private set; }

        public void Initialize(IBasco<TTransitionTrigger> subBasco, IState state)
        {
            this.basco = subBasco;
            this.baseState = state;
        }

        public void Execute()
        {
            this.basco.Start();
            this.baseState.Execute();
            this.basco.Stop();
        }
    }
}