namespace Basco
{
    using System;
    using System.Collections.Generic;

    public class BascoExecutor<TTransitionTrigger> : IBascoExecutor<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBascoStateCache bascoStateCache;
        private readonly Dictionary<Type, IStateTransitions<TTransitionTrigger>> transitionPool;

        public BascoExecutor(IBascoStateCache bascoStateCache)
        {
            this.bascoStateCache = bascoStateCache;
            this.transitionPool = new Dictionary<Type, IStateTransitions<TTransitionTrigger>>();
        }

        public event EventHandler StateEntered;

        public event EventHandler StateExiting;
        
        public event EventHandler StateChanged;

        public IState CurrentState { get; private set; }

        public void InitializeWithStartState<TState>() where TState : class, IState
        {
            this.bascoStateCache.Initialize();

            var stateType = typeof(TState);
            this.CurrentState = this.bascoStateCache.GetState(stateType);
            if (this.CurrentState == null)
            {
                throw new BascoException(string.Format("No valid start state. Check configuration (IBascoConfigurator) of state [{0}] !", stateType));
            }
        }

        public void AddStateTransitions<TState>(StateTransitions<TTransitionTrigger> stateTransitions)
            where TState : class, IState
        {
            this.transitionPool.Add(typeof(TState), stateTransitions);
        }

        public void Start()
        {
            this.EnterState();
            this.ExecuteAndRaiseStateChanged();
        }

        public void Stop()
        {
            this.ExitState();
            this.OnStateChanged();
        }

        public IState RetrieveState<TState>() where TState : class, IState
        {
            return this.bascoStateCache.GetState(typeof(TState));
        }

        public void ChangeState(TTransitionTrigger trigger)
        {
            if (this.CurrentState == null)
            {
                return;
            }

            IState nextState = this.GetNextState(trigger); 
            if (nextState == null)
            {
                return;
            }

            this.ExitState();
            this.CurrentState = nextState;
            this.EnterState();
            this.ExecuteAndRaiseStateChanged();
        }

        protected void OnStateEntered()
        {
            if (this.StateEntered != null)
            {
                this.StateEntered(this, new EventArgs());
            }
        }

        protected void OnStateExiting()
        {
            if (this.StateExiting != null)
            {
                this.StateExiting(this, new EventArgs());
            }
        }

        protected void OnStateChanged()
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new EventArgs());
            }
        }

        private void ExecuteAndRaiseStateChanged()
        {
            this.CurrentState.Execute();
            this.OnStateChanged();
        }

        private void EnterState()
        {
            var enterableState = this.CurrentState as IStateEnter;
            if (enterableState == null)
            {
                return;
            }

            enterableState.Enter();
            this.OnStateEntered();
        }

        private void ExitState()
        {
            var exitableState = this.CurrentState as IStateExit;
            if (exitableState == null)
            {
                return;
            }

            this.OnStateExiting();
            exitableState.Exit();
        }

        private IState GetNextState(TTransitionTrigger trigger)
        {
            if (this.transitionPool.Count == 0)
            {
                //// TODO: throw exception
                return null;
            }

            IStateTransitions<TTransitionTrigger> stateTransitions;
            try
            {
                stateTransitions = this.transitionPool[this.CurrentState.GetType()];
            }
            catch (KeyNotFoundException ex)
            {
                throw new BascoException(string.Format("The state {0} is not configured correctly. Configure transitions for this state in your IBascoConfigurator properly!", this.CurrentState), ex);
            }
            
            Type nextStateType = stateTransitions.GetNextStateType(trigger);
            IState nextState = this.bascoStateCache.GetState(nextStateType);

            return nextState;
        }
    }
}