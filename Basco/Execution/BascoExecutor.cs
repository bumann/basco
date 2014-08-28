namespace Basco.Execution
{
    using System;
    using Basco.Log;

    public class BascoExecutor<TTransitionTrigger> : IBascoExecutor<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBascoStatesProvider<TTransitionTrigger> bascoStatesProvider;
        private readonly IBascoStateEnterExecutor bascoStateEnterExecutor;
        private readonly IBascoStateExitExecutor bascoStateExitExecutor;
        private Type initialStateType;

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

        public bool AlwaysStartWithInitialState { get; set; }

        public void Initialize(Type startStateType)
        {
            this.initialStateType = startStateType;

            if (this.initialStateType == null)
            {
                var bascoException = new BascoException(string.Format("No valid start state type [{0}] provided. Provide valid start state type in Initialize()!", this.initialStateType));
                Logger.LogError(bascoException, "state machine execution error");
                throw bascoException;
            }

            this.CurrentState = this.bascoStatesProvider.Retrieve(this.initialStateType);
            if (this.CurrentState == null)
            {
                var bascoException = new BascoException(string.Format("No valid start state found. Check initialization with states and start state type [{0}] !", this.initialStateType));
                Logger.LogError(bascoException, "state machine execution error");
                throw bascoException;
            }
        }

        public void Start()
        {
            Logger.LogDebug("state machine started.");

            if (this.AlwaysStartWithInitialState)
            {
                this.CurrentState = this.bascoStatesProvider.Retrieve(this.initialStateType);
            }

            this.bascoStateEnterExecutor.Enter(this.CurrentState);
            this.RaiseStateChangedAndExecute();
        }

        public void Stop()
        {
            var compositeState = this.CurrentState as IBascoCompositeState<TTransitionTrigger>;
            if (compositeState != null)
            {
                compositeState.Basco.Stop();
            }

            this.OnStateChanged();

            Logger.LogDebug("state machine stopped.");
        }

        public IState RetrieveState<TState>() where TState : class, IState
        {
            return this.bascoStatesProvider.Retrieve(typeof(TState));
        }

        public void ChangeState(TTransitionTrigger trigger)
        {
            Logger.LogDebug("state machine triggered with {0}.", trigger);

            if (this.CurrentState == null)
            {
                return;
            }

            IState nextState = this.bascoStatesProvider.RetrieveNext(this.CurrentState, trigger); 
            if (nextState == null)
            {
                return;
            }

            Logger.LogDebug("state machine exiting state {0}.", this.CurrentState);
            this.bascoStateExitExecutor.Exit(this.CurrentState);
            this.CurrentState = nextState;
            Logger.LogDebug("state machine entering state {0}.", this.CurrentState);
            this.bascoStateEnterExecutor.Enter(this.CurrentState);
            this.RaiseStateChangedAndExecute();
            Logger.LogDebug("state machine entered state {0}.", this.CurrentState);
        }

        private void RaiseStateChangedAndExecute()
        {
            this.OnStateChanged();
            this.CurrentState.Execute();
        }

        private void OnStateChanged()
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new EventArgs());
            }
        }
    }
}