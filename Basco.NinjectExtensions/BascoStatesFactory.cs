namespace Basco.NinjectExtensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Ninject;
    using Ninject.Syntax;

    public class BascoStatesFactory : IBascoStatesFactory
    {
        private readonly IResolutionRoot resolutionRoot;

        public BascoStatesFactory(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public IEnumerable<IState> CreateStates()
        {
            return this.resolutionRoot
                .GetAll<IState>()
                .ToList();
        }
    }
}