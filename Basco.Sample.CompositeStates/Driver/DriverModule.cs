namespace Basco.Sample.CompositeStates.Driver
{
    using Basco.Sample.CompositeStates.Driver.States;
    using Ninject.Extensions.NamedScope;
    using Ninject.Modules;

    public class DriverModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDriver>().To<Driver>()
                .DefinesNamedScope(Driver.DriverScope);
        }
    }
}