namespace Basco.Sample.UsingNinject.Driver.States
{
    using Basco;
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
            this.BindBasco(basco => basco
                .WithConfigurator<StateMachineConfigurator>()
                .WithModuleController()
                .InNamedScope(Driver.DriverScope));
        }

        private void BindStates()
        {
            this.Bind<IConnectedState, IState>().To<ConnectedState>()
                .InNamedScope(Driver.DriverScope);

            this.Bind<IProcessingState, IState>().To<ProcessingState>()
                .InNamedScope(Driver.DriverScope);

            this.Bind<IErrorState, IState>().To<ErrorState>()
                .InNamedScope(Driver.DriverScope);

            //// alternative: no named scope binding
            //// this.Bind<IConnectedState, IState>().To<ConnectedState>();
            //// this.Bind<IProcessingState, IState>().To<ProcessingState>();
            //// this.Bind<IErrorState, IState>().To<ErrorState>();
        }
    }
}