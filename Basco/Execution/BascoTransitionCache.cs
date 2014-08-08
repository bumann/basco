namespace Basco.Execution
{
    using System;
    using System.Collections.Generic;

    public class BascoTransitionCache<TTransitionTrigger> : IBascoTransitionCache<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        public BascoTransitionCache()
        {
            this.TransitionPool = new Dictionary<Type, IStateTransitions<TTransitionTrigger>>();
        }

        public Dictionary<Type, IStateTransitions<TTransitionTrigger>> TransitionPool { get; private set; }

        public void Add(Type type, StateTransitions<TTransitionTrigger> stateTransitions)
        {
            this.TransitionPool.Add(type, stateTransitions);
        }

        public Type RetrieveNextStateType(IState state, TTransitionTrigger trigger)
        {
            if (this.TransitionPool.Count == 0)
            {
                return null;
            }

            IStateTransitions<TTransitionTrigger> stateTransitions;
            try
            {
                stateTransitions = this.TransitionPool[state.GetType()];
            }
            catch (KeyNotFoundException ex)
            {
                throw new BascoException(string.Format("The state {0} is not configured correctly. Configure transitions for this state in your IBascoConfigurator properly! Check initial state!", state), ex);
            }

            return stateTransitions.GetNextStateType(trigger);
        }
    }
}