namespace Basco
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appccelerate.AsyncModule;

    public class Basco<TTransitionTrigger> : IBasco<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IModuleController moduleController;
        private readonly IBascoExecutor<TTransitionTrigger> bascoExecutor;
        private readonly IEnumerable<IState<TTransitionTrigger>> states;

        public Basco(IModuleController moduleController, IBascoConfigurator<TTransitionTrigger> bascoConfigurator, IBascoExecutor<TTransitionTrigger> bascoExecutor)
        {
            this.moduleController = moduleController;
            this.bascoExecutor = bascoExecutor;

            this.moduleController.Initialize(this, 1, false, "Basco");
            this.bascoExecutor.StateChanged += this.OnStateChanged;
            this.states = bascoConfigurator.Configurate();
        }

        public event EventHandler StateChanged;

        public bool IsRunning { get; private set; }

        public IState<TTransitionTrigger> CurrentState
        {
            get { return this.bascoExecutor.CurrentState; }
        }

        public void Start<TState>() where TState : IState<TTransitionTrigger>
        {
            if (this.IsRunning)
            {
                throw new BascoException("The state machine was already started!");
            }

            this.IsRunning = true;

            var startState = this.states.SingleOrDefault(x => x.GetType() == typeof(TState));
            if (startState == null)
            {
                return;
            }

            this.bascoExecutor.Start(startState);
            this.moduleController.Start();
        }

        public void Stop()
        {
           this.bascoExecutor.Stop();
           this.moduleController.Stop();
           this.IsRunning = false;
        }

        public IState<TTransitionTrigger> RetrieveState<TState>() where TState : IState<TTransitionTrigger>
        {
            IState<TTransitionTrigger> state = this.states.SingleOrDefault(x => x.GetType() == typeof(TState));
            return state;
        }

        public void Trigger(TTransitionTrigger trigger)
        {
            this.moduleController.EnqueueMessage(trigger);
        }

        [MessageConsumer]
        public void TriggerConsumer(TTransitionTrigger trigger)
        {
            this.bascoExecutor.ChangeState(trigger);
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