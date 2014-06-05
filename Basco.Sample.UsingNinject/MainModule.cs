namespace Basco.Sample.UsingNinject
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