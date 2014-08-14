namespace Basco.Sample.CompositeStates.Driver
{
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