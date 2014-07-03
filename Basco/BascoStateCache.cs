namespace Basco
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BascoStateCache : IBascoStateCache
    {
        private readonly IBascoStatesFactory statesBascoFactory;
        private ICollection<IState> states;

        public BascoStateCache(IBascoStatesFactory statesBascoFactory)
        {
            this.statesBascoFactory = statesBascoFactory;
        }

        public void Initialize()
        {
            this.states = this.statesBascoFactory.CreateStates().ToList();

            if (this.states.Count == 0)
            {
                throw new BascoException("BascoStateCache initialization failed. No states could be created. Concrete BascoStatesFactory should return all available IStates!");
            }
        }

        public IState GetState(Type stateType)
        {
            return this.states.SingleOrDefault(x => x.GetType() == stateType);
        }
    }
}