namespace Basco
{
    using System;
    using Basco.Configuration;
    using Basco.Execution;
    using Scyano;

    public class Basco<TTransitionTrigger> : IBasco<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IScyano scyano;
        private readonly IBascoStateCache<TTransitionTrigger> bascoStateCache;
        private readonly IBascoConfigurator<TTransitionTrigger> bascoConfigurator;

        public Basco(
            IScyano scyano,
            IBascoStateCache<TTransitionTrigger> bascoStateCache,
            IBascoTransitionCache<TTransitionTrigger> transitionCache,
            IBascoExecutor<TTransitionTrigger> bascoExecutor,
            IBascoConfigurator<TTransitionTrigger> bascoConfigurator = null)
        {
            this.scyano = scyano;
            this.bascoStateCache = bascoStateCache;
            this.TransitionCache = transitionCache;
            this.BascoExecutor = bascoExecutor;
            this.bascoConfigurator = bascoConfigurator;
        }

        public event EventHandler StateChanged;

        public bool IsInitialized { get; private set; }

        public bool IsRunning { get; private set; }

        public IState CurrentState
        {
            get { return this.BascoExecutor.CurrentState; }
        }

        public IBascoTransitionCache<TTransitionTrigger> TransitionCache { get; private set; }

        public IBascoExecutor<TTransitionTrigger> BascoExecutor { get; private set; }

        public void AddTransitionCache(Type type, StateTransitions<TTransitionTrigger> stateTransitions) 
        {
            this.TransitionCache.Add(type, stateTransitions);
        }

        public void InitializeWithStartState<TState>() where TState : class, IState
        {
            this.scyano.Initialize(this);
            this.bascoStateCache.Initialize(this);
            this.BascoExecutor.InitializeWithStartState<TState>(this);

            //// TODO
            this.BascoExecutor.StateChanged += this.OnStateChanged;

            if (this.bascoConfigurator != null)
            {
                this.bascoConfigurator.Configurate(this);
            }

            this.IsInitialized = true;
        }

        public void Start()
        {
            if (!this.IsInitialized)
            {
                throw new BascoException("The state machine was not initialized. Do call Basco.InitializeWithStartState<TStartState>()!");
            }

            if (this.IsRunning)
            {
                throw new BascoException("The state machine was already started. Do not call Basco.Start() twice!");
            }

            this.IsRunning = true;
            try
            {
                this.scyano.Start();
                this.BascoExecutor.Start();
            }
            catch (Exception)
            {
                this.scyano.Stop();
                this.IsRunning = false;
                throw;
            }
        }

        public void Stop()
        {
            this.IsRunning = false;
            this.BascoExecutor.Stop();
            this.scyano.Stop();
        }

        public IState RetrieveState<TState>() where TState : class, IState
        {
            return this.BascoExecutor.RetrieveState<TState>();
        }

        public void Trigger(TTransitionTrigger trigger)
        {
            this.scyano.Enqueue(trigger);
        }

        [MessageConsumer]
        public void TriggerConsumer(TTransitionTrigger trigger)
        {
            this.BascoExecutor.ChangeState(trigger);
        }

        protected void OnStateChanged(object sender, EventArgs e)
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new EventArgs());
            }
        }
    }
}