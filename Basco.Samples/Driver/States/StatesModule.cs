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
                .InNamedScope(Driver.DriverScope)
                .WithConfigurator<StateMachineConfigurator>();
        }

        private void BindStates()
        {
            this.Bind<IConnectedState, IState>().To<ConnectedState>()
                .InNamedScope(Driver.DriverScope);

            this.Bind<IProcessingState, IState>().To<ProcessingState>()
                .InNamedScope(Driver.DriverScope);

            this.Bind<IErrorState, IState>().To<ErrorState>()
                .InNamedScope(Driver.DriverScope);
        }
    }
}