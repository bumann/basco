namespace Basco.Execution
{
    using System;
    using System.Collections.Generic;

    public class BascoTransitionCache<TTransitionTrigger> : IBascoTransitionCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly Dictionary<Type, IStateTransitions<TTransitionTrigger>> transitionPool;

        public BascoTransitionCache()
        {
            this.transitionPool = new Dictionary<Type, IStateTransitions<TTransitionTrigger>>();
        }

        public void Add(Type type, StateTransitions<TTransitionTrigger> stateTransitions)
        {
            this.transitionPool.Add(type, stateTransitions);
        }

        public Type RetrieveNextStateType(IState state, TTransitionTrigger trigger)
        {
            if (this.transitionPool.Count == 0)
            {
                return null;
            }

            IStateTransitions<TTransitionTrigger> stateTransitions;
            try
            {
                stateTransitions = this.transitionPool[state.GetType()];
            }
            catch (KeyNotFoundException ex)
            {
                throw new BascoException(string.Format("The state {0} is not configured correctly. Configure transitions for this state in your IBascoConfigurator properly! Check initial state!", state), ex);
            }

            return stateTransitions.GetNextStateType(trigger);
        }
    }
}