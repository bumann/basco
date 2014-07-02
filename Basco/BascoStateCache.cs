namespace Basco
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BascoStateCache : IBascoStateCache
    {
        private readonly IBascoFactory statesBascoFactory;
        private ICollection<IState> states;

        public BascoStateCache(IBascoFactory statesBascoFactory)
        {
            this.statesBascoFactory = statesBascoFactory;
        }

        public void Initialize()
        {
            this.states = this.statesBascoFactory.CreateAll<IState>().ToList();

            if (this.states.Count == 0)
            {
                throw new BascoException("BascoStateCache initialization failed. No states could be created. BascoFactory should return all available IStates!");
            }
        }

        public IState GetState(Type stateType)
        {
            return this.states.SingleOrDefault(x => x.GetType() == stateType);
        }
    }
}