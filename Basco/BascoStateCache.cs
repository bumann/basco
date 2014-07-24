namespace Basco
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BascoStateCache<TTransitionTrigger> : IBascoStateCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBascoStatesFactory<TTransitionTrigger> statesBascoFactory;
        private ICollection<IState> states;

        public BascoStateCache(IBascoStatesFactory<TTransitionTrigger> statesBascoFactory)
        {
            this.statesBascoFactory = statesBascoFactory;
        }

        public void Initialize(IBasco<TTransitionTrigger> basco)
        {
            this.states = this.statesBascoFactory.CreateStates(basco).ToList();

            if (this.states.Count == 0)
            {
                throw new BascoException("BascoStateCache initialization failed. No states could be created. Concrete BascoStatesFactory should return all available IStates!");
            }
        }

        public IState RetrieveState(Type stateType)
        {
            return this.states.SingleOrDefault(x => x.GetType() == stateType);
        }
    }
}