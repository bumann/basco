namespace Basco
{
    using System;
    using Scyano;

    public class Basco<TTransitionTrigger> : IBasco<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IScyano scyano;
        private readonly IBascoConfigurator<TTransitionTrigger> bascoConfigurator;

        public Basco(IScyano scyano, IBascoConfigurator<TTransitionTrigger> bascoConfigurator, IBascoExecutor<TTransitionTrigger> bascoExecutor)
        {
            this.scyano = scyano;
            this.bascoConfigurator = bascoConfigurator;
            this.BascoExecutor = bascoExecutor;
        }

        public event EventHandler StateChanged;

        public bool IsInitialized { get; private set; }

        public bool IsRunning { get; private set; }

        public IState CurrentState
        {
            get { return this.BascoExecutor.CurrentState; }
        }

        public IBascoExecutor<TTransitionTrigger> BascoExecutor { get; private set; }

        public void Initialize()
        {
            this.scyano.Initialize(this);
            this.BascoExecutor.Initialize();
            this.BascoExecutor.StateChanged += this.OnStateChanged;

            this.bascoConfigurator.Configurate(this);
            this.IsInitialized = true;
        }

        public void Start<TState>() where TState : class, IState
        {
            if (!this.IsInitialized)
            {
                throw new BascoException("The state machine was not initialized. Do call Basco.Initialize()!");
            }

            if (this.IsRunning)
            {
                throw new BascoException("The state machine was already started. Do not call Start() twice!");
            }

            if (!this.BascoExecutor.Start<TState>())
            {
                throw new BascoException(string.Format("The state machine could not be started. Check configuration (IBascoConfigurator) or start state [{0}]!", typeof(TState)));
            }

            this.scyano.Start();
            this.IsRunning = true;
        }

        public void Stop()
        {
           this.BascoExecutor.Stop();
           this.scyano.Stop();
           this.IsRunning = false;
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

        private void OnStateChanged(object sender, EventArgs e)
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new EventArgs());
            }
        }
    }
}