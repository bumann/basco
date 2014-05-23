﻿namespace Basco.Samples
{
    using Ninject.Modules;

    public class DriverModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDriverControlModel>().To<DriverControlModel>();
        }
    }
}