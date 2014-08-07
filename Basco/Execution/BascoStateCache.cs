namespace Basco.Execution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BascoStateCache<TTransitionTrigger> : IBascoStateCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly ICollection<IState> states;

        public BascoStateCache(IEnumerable<IState> states)
        {
            this.states = states.ToList();
        }

        public void Initialize(IBasco<TTransitionTrigger> basco)
        {
            foreach (var stateUsingBasco in this.states.OfType<IStateUsingBasco<TTransitionTrigger>>())
            {
                stateUsingBasco.Basco = basco;
            }

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