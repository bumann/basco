namespace Basco.Samples
{
    using Ninject.Modules;

    public class DriverModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDriver>().To<Driver>();
            this.Bind<IDriverViewModel>().To<DriverViewModel>();
        }
    }
}