namespace Basco
{
    using System;

    public class BascoCompositeState<TTransitionTrigger, TState> : IBascoCompositeState<TTransitionTrigger>
        where TTransitionTrigger : IComparable
        where TState : IState
    {
        private IState baseState;

        public BascoCompositeState()
        {
            this.BaseStateType = typeof(TState);
        }

        public Type BaseStateType { get; private set; }

        public IBasco<TTransitionTrigger> Basco { get; private set; }

        public void Initialize(IBasco<TTransitionTrigger> subBasco, IState state)
        {
            this.Basco = subBasco;
            this.baseState = state;
            if (this.baseState == null)
            {
                throw new BascoException("Composite state not found! Provide necessary state or fix bindings.");
            }
        }

        public void Enter()
        {
            this.Basco.Start();
            var enterableState = this.baseState as IStateEnter;
            if (enterableState != null)
            {
                enterableState.Enter();
            }
        }

        public void Execute()
        {
            this.baseState.Execute();
        }

        public void Exit()
        {
            var exitableState = this.baseState as IStateExit;
            if (exitableState != null)
            {
                exitableState.Exit(); 
            }

            this.Basco.Stop();
        }
    }
}