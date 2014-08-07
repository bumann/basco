namespace Basco.Execution
{
    using System;

    public class BascoExecutor<TTransitionTrigger> : IBascoExecutor<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBascoStatesProvider<TTransitionTrigger> bascoStatesProvider;
        private readonly IBascoStateEnterExecutor bascoStateEnterExecutor;
        private readonly IBascoStateExitExecutor bascoStateExitExecutor;

        public BascoExecutor(
            IBascoStatesProvider<TTransitionTrigger> bascoStatesProvider,
            IBascoStateEnterExecutor bascoStateEnterExecutor,
            IBascoStateExitExecutor bascoStateExitExecutor)
        {
            this.bascoStatesProvider = bascoStatesProvider;
            this.bascoStateEnterExecutor = bascoStateEnterExecutor;
            this.bascoStateExitExecutor = bascoStateExitExecutor;
        }

        public event EventHandler StateChanged;

        public IState CurrentState { get; private set; }

        public void InitializeWithStartState<TState>(IBasco<TTransitionTrigger> basco)
            where TState : class, IState
        {
            this.CurrentState = this.bascoStatesProvider.Retrieve(typeof(TState));

            if (this.CurrentState == null)
            {
                throw new BascoException(string.Format("No valid start state. Check configuration (IBascoConfigurator) of state [{0}] !", typeof(TState)));
            }
        }

        public void Start()
        {
            this.bascoStateEnterExecutor.Enter(this.CurrentState);
            this.RaiseStateChangedAndExecute();
        }

        public void Stop()
        {
            this.OnStateChanged();
        }

        public IState RetrieveState<TState>() where TState : class, IState
        {
            return this.bascoStatesProvider.Retrieve(typeof(TState));
        }

        public void ChangeState(TTransitionTrigger trigger)
        {
            if (this.CurrentState == null)
            {
                return;
            }

            IState nextState = this.bascoStatesProvider.RetrieveNext(this.CurrentState, trigger); 
            if (nextState == null)
            {
                return;
            }

            this.bascoStateExitExecutor.Exit(this.CurrentState);
            this.CurrentState = nextState;
            this.bascoStateEnterExecutor.Enter(this.CurrentState);
            this.RaiseStateChangedAndExecute();
        }

        protected void OnStateChanged()
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new EventArgs());
            }
        }

        private void RaiseStateChangedAndExecute()
        {
            this.OnStateChanged();
            this.CurrentState.Execute();
        }
    }
}