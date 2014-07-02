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

        public event EventHandler StateChanged;

        public IState CurrentState { get; private set; }

        public void AddStateTransitions<TState>(StateTransitions<TTransitionTrigger> stateTransitions)
            where TState : class, IState
        {
            this.transitionPool.Add(typeof(TState), stateTransitions);
        }

        public bool Start<TState>() where TState : class, IState
        {
            this.bascoStateCache.Initialize();
            this.CurrentState = this.bascoStateCache.GetState(typeof(TState));
            if (this.CurrentState == null)
            {
                return false;
            }

            this.EnterState();
            this.ExecuteAndRaiseStateChanged();
            return true;
        }

        public void Stop()
        {
            this.ExitState();
            this.CurrentState = null;
            this.RaiseStateChanged();
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

        private IState GetNextState(TTransitionTrigger trigger)
        {
            if (this.transitionPool.Count == 0)
            {
                return null;
            }

            IStateTransitions<TTransitionTrigger> stateTransitions = this.transitionPool[this.CurrentState.GetType()];
            Type nextStateType = stateTransitions.GetNextStateType(trigger);
            IState nextState = this.bascoStateCache.GetState(nextStateType);

            return nextState;
        }
    }
}