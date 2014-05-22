namespace Basco
{
    using System;

    public class BascoExecutor<TTransitionTrigger> : IBascoExecutor<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        public event EventHandler StateChanged;

        public IState<TTransitionTrigger> CurrentState { get; private set; }

        public void Start(IState<TTransitionTrigger> initialState)
        {
            this.CurrentState = initialState;
            this.EnterState();
            this.ExecuteAndRaiseStateChanged();
        }

        public void Stop()
        {
            this.ExitState();
            this.CurrentState = null;
            this.RaiseStateChanged();
        }

        public void ChangeState(TTransitionTrigger trigger)
        {
            if (this.CurrentState == null)
            {
                return;
            }

            IState<TTransitionTrigger> nextState = this.CurrentState.Transitions.GetNextState(trigger);
            if (nextState == null)
            {
                return;
            }

            this.ExitState();
            this.CurrentState = nextState;
            this.EnterState();
            this.ExecuteAndRaiseStateChanged();
        }

        private void ExecuteAndRaiseStateChanged()
        {
            this.CurrentState.Execute();
            this.RaiseStateChanged();
        }

        private void EnterState()
        {
            var enterableState = this.CurrentState as IStateEnter;
            if (enterableState != null)
            {
                enterableState.Enter();
            }
        }

        private void ExitState()
        {
            var exitableState = this.CurrentState as IStateExit;
            if (exitableState != null)
            {
                exitableState.Exit();
            }
        }

        private void RaiseStateChanged()
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new EventArgs());
            }
        }
    }
}