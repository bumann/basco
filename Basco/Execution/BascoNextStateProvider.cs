namespace Basco.Execution
{
    using System;

    public class BascoNextStateProvider<TTransitionTrigger> : IBascoNextStateProvider<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBascoStateCache<TTransitionTrigger> bascoStateCache;
        private readonly IBascoTransitionCache<TTransitionTrigger> bascoTransitionCache;

        public BascoNextStateProvider(IBascoStateCache<TTransitionTrigger> bascoStateCache, IBascoTransitionCache<TTransitionTrigger> bascoTransitionCache)
        {
            this.bascoStateCache = bascoStateCache;
            this.bascoTransitionCache = bascoTransitionCache;
        }

        public IState RetrieveNext(IState state, TTransitionTrigger trigger)
        {
            Type nextStateType = this.bascoTransitionCache.RetrieveNextStateType(state, trigger);
            IState nextState = this.bascoStateCache.RetrieveState(nextStateType);

            return nextState;
        }
    }
}