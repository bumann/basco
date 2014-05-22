namespace Basco
{
    using System;
    using Appccelerate.AsyncModule;

    public class Basco<TTransitionTrigger> : IBasco<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IModuleController moduleController;
        private readonly IBascoConfigurator bascoConfigurator;
        private readonly IBascoExecutor<TTransitionTrigger> bascoExecutor;

        public Basco(IBascoConfigurator bascoConfigurator, IBascoExecutor<TTransitionTrigger> bascoExecutor, IModuleController moduleController)
        {
            this.moduleController = moduleController;
            this.moduleController.Initialize(this, 1, false, "Basco");
            this.bascoConfigurator = bascoConfigurator;
            this.bascoExecutor = bascoExecutor;
        }

        public bool IsRunning { get; private set; }

        public void Start(IState<TTransitionTrigger> initialState)
        {
            if (this.IsRunning)
            {
                throw new BascoException("The state machine was already started!");
            }

            this.IsRunning = true;
            this.bascoConfigurator.Configurate();
            this.bascoExecutor.Start(initialState);
            this.moduleController.Start();
        }

        public void Stop()
        {
           this.bascoExecutor.Stop();
           this.moduleController.Stop();
           this.IsRunning = false;
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
    }
}