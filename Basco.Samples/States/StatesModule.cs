namespace Basco.Samples.States
{
    using Appccelerate.AsyncModule;
    using Ninject.Modules;

    public class StatesModule : NinjectModule
    {
        public override void Load()
        {
            this.BindBasco();
            this.BindStates();
        }

        private void BindBasco()
        {
            // TODO: simpler binding - convenient usage
            this.Bind<IModuleController>().To<ModuleController>();
            this.Bind<IBascoConfigurator<Transitions>>().To<StateMachineConfigurator>();
            this.Bind<IBasco<Transitions>>().To<Basco<Transitions>>();
            this.Bind<IBascoExecutor<Transitions>>().To<BascoExecutor<Transitions>>();
            this.Bind<ITransitionPool<Transitions>>().To<TransitionPool<Transitions>>();
        }

        private void BindStates()
        {
            this.Bind<IConnectedState>().To<ConnectedState>();
            this.Bind<IProcessingState>().To<ProcessingState>();
            this.Bind<IErrorState>().To<ErrorState>();
        }
    }
}