namespace Basco.Samples.States
{
    using Basco.NinjectExtensions;
    using Ninject.Modules;

    public class StatesModule : NinjectModule
    {
        public override void Load()
        {
            this.BindStateMachine();
            this.BindStates();
        }

        private void BindStateMachine()
        {
            // TODO: simplify
            this.BindBasco().ForTriggers<TransitionTrigger>();
            this.Bind<IBascoConfigurator<TransitionTrigger>>().To<StateMachineConfigurator>();
        }

        private void BindStates()
        {
            this.Bind<IConnectedState, IState>().To<ConnectedState>();
            this.Bind<IProcessingState, IState>().To<ProcessingState>();
            this.Bind<IErrorState, IState>().To<ErrorState>();
        }
    }
}