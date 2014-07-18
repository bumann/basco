namespace Basco.Execution
{
    using System;

    public class BascoExecutor<TTransitionTrigger> : IBascoExecutor<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBascoStateCache bascoStateCache;
        private readonly IBascoNextStateProvider<TTransitionTrigger> bascoNextStateProvider;
        private readonly IBascoStateEnterExecutor bascoStateEnterExecutor;
        private readonly IBascoStateExitExecutor bascoStateExitExecutor;

        public BascoExecutor(
            IBascoStateCache bascoStateCache,
            IBascoNextStateProvider<TTransitionTrigger> bascoNextStateProvider,
            IBascoStateEnterExecutor bascoStateEnterExecutor,
            IBascoStateExitExecutor bascoStateExitExecutor)
        {
            this.bascoStateCache = bascoStateCache;
            this.bascoNextStateProvider = bascoNextStateProvider;
            this.bascoStateEnterExecutor = bascoStateEnterExecutor;
            this.bascoStateExitExecutor = bascoStateExitExecutor;
        }

        public event EventHandler StateChanged;

        public IState CurrentState { get; private set; }

        public void InitializeWithStartState<TState>() where TState : class, IState
        {
            this.bascoStateCache.Initialize();

            var stateType = typeof(TState);
            this.CurrentState = this.bascoStateCache.RetrieveState(stateType);
            if (this.CurrentState == null)
            {
                throw new BascoException(string.Format("No valid start state. Check configuration (IBascoConfigurator) of state [{0}] !", stateType));
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
            return this.bascoStateCache.RetrieveState(typeof(TState));
        }

        public void ChangeState(TTransitionTrigger trigger)
        {
            if (this.CurrentState == null)
            {
                return;
            }

            IState nextState = this.bascoNextStateProvider.RetrieveNext(this.CurrentState, trigger); 
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