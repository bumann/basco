namespace Basco.NinjectExtensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Ninject;
    using Ninject.Syntax;

    public class BascoFactory : IBascoFactory
    {
        private readonly IResolutionRoot resolutionRoot;

        public BascoFactory(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public IEnumerable<T> CreateAll<T>()
        {
            return this.resolutionRoot
                .GetAll<T>()
                .ToList();
        }
    }
}