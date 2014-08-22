namespace Basco.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class StateConfigurator<TTransitionTrigger> : IStateConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IBascoInternal<TTransitionTrigger> basco;
        private readonly StateTransitions<TTransitionTrigger> stateTransitions;
        private TTransitionTrigger nextTrigger;
        private IBascoCompositeState<TTransitionTrigger> compositeState;
        private IBascoInternal<TTransitionTrigger> subBasco;

        public StateConfigurator(IBascoInternal<TTransitionTrigger> basco)
        {
            this.basco = basco;
            this.stateTransitions = new StateTransitions<TTransitionTrigger>();
        }

        public void ForState<TState>()
            where TState : class, IState
        {
            this.basco.AddTransitionCache(typeof(TState), this.stateTransitions);
        }

        public void ForCompositeState<TState>()
            where TState : class, IState
        {
            this.compositeState = new BascoCompositeState<TTransitionTrigger, TState>();
            this.basco.StatesCache.AddComposite(this.compositeState);
            this.ForState<TState>();
        }

        public IStateConfigurator<TTransitionTrigger> WithSubStates(Action<IBascoInternal<TTransitionTrigger>> bascoConfigurator)
        {
            if (this.compositeState == null)
            {
                throw new BascoException("No composite state defined. Use ForComposite<YourState, YourTrigger> in your configuration!");
            }

            this.subBasco = BascoFactory.Create<TTransitionTrigger>();
            bascoConfigurator(subBasco);
            Type startStateType = subBasco.TransitionCache.TransitionPool.Keys.FirstOrDefault();
            List<Type> stateTypes = subBasco.TransitionCache.TransitionPool.Keys.ToList();
            var states = this.basco.StatesCache.RetrieveStates(stateTypes);
            subBasco.Initialize(states, startStateType);
            this.basco.AddHierchary(subBasco);
            var baseState = this.basco.StatesCache.RetrieveBaseState(this.compositeState.BaseStateType);
            this.compositeState.Initialize(subBasco, baseState);

            return this;
        }

        public IStateConfigurator<TTransitionTrigger> OnEnterAlwaysStartWithInitialState()
        {
            this.subBasco.BascoExecutor.AlwaysStartWithInitialState = true;
            return this;
        }

        public IStateConfigurator<TTransitionTrigger> When(TTransitionTrigger trigger)
        {
            this.nextTrigger = trigger;
            return this;
        }

        public IStateConfigurator<TTransitionTrigger> Goto<TTargetState>()
            where TTargetState : class
        {
            this.stateTransitions.Add(this.nextTrigger, typeof(TTargetState));
            return this;
        }
    }
}