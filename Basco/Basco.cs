namespace Basco
{
    using System;
    using Scyano;

    public class Basco<TTransitionTrigger> : IBasco<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IScyano scyano;

        public Basco(IScyano scyano, IBascoConfigurator<TTransitionTrigger> bascoConfigurator, IBascoExecutor<TTransitionTrigger> bascoExecutor)
        {
            this.scyano = scyano;
            this.BascoExecutor = bascoExecutor;

            this.scyano.Initialize(this);
            this.BascoExecutor.Initialize();
            this.BascoExecutor.StateChanged += this.OnStateChanged;

            bascoConfigurator.Configurate(this);
        }

        public event EventHandler StateChanged;

        public bool IsRunning { get; private set; }

        public IState CurrentState
        {
            get { return this.BascoExecutor.CurrentState; }
        }

        public IBascoExecutor<TTransitionTrigger> BascoExecutor { get; private set; }

        public void Start<TState>() where TState : class, IState
        {
            if (this.IsRunning)
            {
                throw new BascoException("The state machine was already started!");
            }

            if (!this.BascoExecutor.Start<TState>())
            {
                return;
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