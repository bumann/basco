namespace Basco.NinjectExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ninject;
    using Ninject.Syntax;

    public class BascoStatesFactory<TTransitionTrigger> : IBascoStatesFactory<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IResolutionRoot resolutionRoot;

        public BascoStatesFactory(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public IEnumerable<IState> CreateStates(IBasco<TTransitionTrigger> basco)
        {
            return this.resolutionRoot
                .GetAll<IState>()
                .ToList();
        }
    }
}