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
            this.bascoConfigurator = bascoConfigurator;
            this.bascoExecutor = bascoExecutor;
        }

        public void Start(IState<TTransitionTrigger> initialState)
        {
            this.bascoExecutor.Start(initialState);
            this.bascoConfigurator.Configurate();
            this.moduleController.Initialize(this, 1, false, "Basco");
            this.moduleController.Start();
        }

        public void Stop()
        {
           this.bascoExecutor.Stop();
           this.moduleController.Stop();
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