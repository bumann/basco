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
            this.Bind<IModuleController>().To<ModuleController>();
            this.Bind<IBascoConfigurator>().To<StateMachineConfigurator>();
            this.Bind<IBasco<Transitions>>().To<Basco<Transitions>>();
            this.Bind<IBascoExecutor<Transitions>>().To<BascoExecutor<Transitions>>().InSingletonScope();
            this.Bind<ITransitionPool<Transitions>>().To<TransitionPool<Transitions>>();
        }

        private void BindStates()
        {
            this.Bind<IConnectedState>().To<ConnectedState>().InSingletonScope();
            this.Bind<IProcessingState>().To<ProcessingState>().InSingletonScope();
            this.Bind<IErrorState>().To<ErrorState>().InSingletonScope();
        }
    }
}