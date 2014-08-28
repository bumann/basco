namespace Basco.Sample.CompositeStates.Driver.States
{
    using Basco.Log;
    using Basco.Log4NetExtensions;
    using Basco.NinjectExtensions;
    using Ninject.Extensions.NamedScope;
    using Ninject.Modules;

    public class StatesModule : NinjectModule
    {
        public override void Load()
        {
            this.BindStateMachine();
            this.BindStates();
            this.BindLogger();
        }

        private void BindStateMachine()
        {
            this.BindBasco(basco => basco
                .InNamedScope(Driver.DriverScope)
                .WithConfigurator<CompositeStateMachineConfigurator>()
                .WithScyano());
        }

        private void BindStates()
        {
            this.Bind<IStateA, IState>().To<StateA>()
                .InNamedScope(Driver.DriverScope);

            this.Bind<IStateB, IState>().To<StateB>()
               .InNamedScope(Driver.DriverScope);

            this.Bind<IStateC, IState>().To<StateC>()
                .InNamedScope(Driver.DriverScope);

            this.Bind<ISubStateD, IState>().To<SubStateD>()
                .InNamedScope(Driver.DriverScope);
                ////.OnActivation(x => x.Initialize());

            this.Bind<ISubStateE, IState>().To<SubStateE>()
                .InNamedScope(Driver.DriverScope);
                ////.OnActivation(x => x.Initialize());

            this.Bind<ISubStateF, IState>().To<SubStateF>()
               .InNamedScope(Driver.DriverScope);

            this.Bind<ISubStateG, IState>().To<SubStateG>()
                .InNamedScope(Driver.DriverScope);

            //// alternative: no named scope binding
            //// this.Bind<IStateA, IState>().To<StateA>();
            //// this.Bind<IStateB, IState>().To<StateB>();
            //// this.Bind<IStateC, IState>().To<StateC>();
        }

        private void BindLogger()
        {
            this.Bind<IBascoLogger>().To<Log4NetLogger>()
                .InNamedScope(Driver.DriverScope);
        }
    }
}