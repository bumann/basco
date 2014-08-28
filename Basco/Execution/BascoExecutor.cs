namespace Basco.Execution
{
    using System;
    using Basco.Log;

    public class BascoExecutor<TTransitionTrigger> : IBascoExecutor<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBascoLogger logger;
        private readonly IBascoStatesProvider<TTransitionTrigger> bascoStatesProvider;
        private readonly IBascoStateEnterExecutor bascoStateEnterExecutor;
        private readonly IBascoStateExitExecutor bascoStateExitExecutor;
        private Type initialStateType;

        public BascoExecutor(
            IBascoLoggerProvider bascoLoggerProvider,
            IBascoStatesProvider<TTransitionTrigger> bascoStatesProvider,
            IBascoStateEnterExecutor bascoStateEnterExecutor,
            IBascoStateExitExecutor bascoStateExitExecutor)
        {
            this.logger = bascoLoggerProvider.ActiveLogger;
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
                this.logger.LogError(bascoException, "state machine execution error");
                throw bascoException;
            }

            this.CurrentState = this.bascoStatesProvider.Retrieve(this.initialStateType);
            if (this.CurrentState == null)
            {
                var bascoException = new BascoException(string.Format("No valid start state found. Check initialization with states and start state type [{0}] !", this.initialStateType));
                this.logger.LogError(bascoException, "state machine execution error");
                throw bascoException;
            }
        }

        public void Start()
        {
            this.logger.LogDebug("state machine started.");

            if (this.AlwaysStartWithInitialState)
            {
                this.CurrentState = this.bascoStatesProvider.Retrieve(this.initialStateType);
            }

            this.EnteringState(this.CurrentState);
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

            this.logger.LogDebug("state machine stopped.");
        }

        public IState RetrieveState<TState>() where TState : class, IState
        {
            return this.bascoStatesProvider.Retrieve(typeof(TState));
        }

        public void ChangeState(TTransitionTrigger trigger)
        {
            this.logger.LogDebug("state machine triggered with [{0}]", trigger);

            if (this.CurrentState == null)
            {
                return;
            }

            IState nextState = this.bascoStatesProvider.RetrieveNext(this.CurrentState, trigger); 
            if (nextState == null)
            {
                return;
            }

            this.logger.LogDebug("state machine exiting state [{0}].", this.CurrentState.GetType().Name);
            this.bascoStateExitExecutor.Exit(this.CurrentState);
            this.CurrentState = nextState;
            this.EnteringState(this.CurrentState);
            this.RaiseStateChangedAndExecute();
        }

        private void EnteringState(IState state)
        {
            this.logger.LogDebug("state machine entering state [{0}].", state.GetType().Name);
            this.bascoStateEnterExecutor.Enter(state);
        }

        private void RaiseStateChangedAndExecute()
        {
            this.OnStateChanged();
            this.CurrentState.Execute();
            this.logger.LogDebug("state machine entered state [{0}].", this.CurrentState.GetType().Name);
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