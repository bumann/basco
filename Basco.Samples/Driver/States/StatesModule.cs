namespace Basco.Samples.Driver.States
{
    using Basco.NinjectExtensions;
    using Ninject.Extensions.NamedScope;
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
            this.BindBasco()
                .InNamedScope(DriverConstants.DriverScope)
                .WithConfigurator<StateMachineConfigurator>();
        }

        private void BindStates()
        {
            this.Bind<IConnectedState, IState>().To<ConnectedState>()
                .InNamedScope(DriverConstants.DriverScope);

            this.Bind<IProcessingState, IState>().To<ProcessingState>()
                .InNamedScope(DriverConstants.DriverScope);

            this.Bind<IErrorState, IState>().To<ErrorState>()
                .InNamedScope(DriverConstants.DriverScope);
        }
    }
}