namespace Basco.Sample.CompositeStates
{
    using Ninject.Modules;

    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDriverControlModel>().To<DriverControlModel>();
        }
    }
}