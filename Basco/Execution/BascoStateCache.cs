namespace Basco.Execution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BascoStateCache<TTransitionTrigger> : IBascoStateCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private ICollection<IState> statesCache;

        public void Initialize(IBasco<TTransitionTrigger> basco, IEnumerable<IState> states)
        {
            this.statesCache = states.ToList();
            if (this.statesCache.Count == 0)
            {
                throw new BascoException("StatesCache initialization failed. No states could be created. Concrete BascoStatesFactory should return all available IStates!");
            }

            foreach (var stateUsingBasco in this.statesCache.OfType<IStateUsingBasco<TTransitionTrigger>>())
            {
                stateUsingBasco.Basco = basco;
            }
        }

        public IState RetrieveState(Type stateType)
        {
            return this.statesCache.SingleOrDefault(x => x.GetType() == stateType);
        }

        public IEnumerable<IState> RetrieveStates(IEnumerable<Type> stateTypes)
        {
            return this.statesCache.Where(x => stateTypes.Contains(x.GetType()));
        }
    }
}