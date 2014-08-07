namespace Basco.Execution
{
    using System;

    public class BascoStatesProvider<TTransitionTrigger> : IBascoStatesProvider<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBascoStateCache<TTransitionTrigger> bascoStateCache;
        private readonly IBascoTransitionCache<TTransitionTrigger> bascoTransitionCache;

        public BascoStatesProvider(IBascoStateCache<TTransitionTrigger> bascoStateCache, IBascoTransitionCache<TTransitionTrigger> bascoTransitionCache)
        {
            this.bascoStateCache = bascoStateCache;
            this.bascoTransitionCache = bascoTransitionCache;
        }

        public IState Retrieve(Type stateType)
        {
            return this.bascoStateCache.RetrieveState(stateType);
        }

        public IState RetrieveNext(IState state, TTransitionTrigger trigger)
        {
            Type nextStateType = this.bascoTransitionCache.RetrieveNextStateType(state, trigger);
            IState nextState = this.bascoStateCache.RetrieveState(nextStateType);

            return nextState;
        }
    }
}