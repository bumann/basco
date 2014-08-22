namespace Basco.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class CompositeStateConfigurator<TTransitionTrigger> : StateConfigurator<TTransitionTrigger>, ICompositeStateConfigurator<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private IBascoCompositeState<TTransitionTrigger> compositeState;
        private IBascoInternal<TTransitionTrigger> subBasco;

        public CompositeStateConfigurator(IBascoInternal<TTransitionTrigger> basco) : base(basco)
        {
        }

        public override void ForState<TState>()
        {
            this.compositeState = new BascoCompositeState<TTransitionTrigger, TState>();
            this.Basco.StatesCache.AddComposite(this.compositeState);
            base.ForState<TState>();
        }

        public ICompositeStateConfigurator<TTransitionTrigger> WithSubStates(Action<IBascoInternal<TTransitionTrigger>> bascoConfigurator)
        {
            if (this.compositeState == null)
            {
                throw new BascoException("No composite state defined. Use ForComposite<YourState, YourTrigger> in your configuration!");
            }

            this.subBasco = BascoFactory.Create<TTransitionTrigger>();
            bascoConfigurator(this.subBasco);
            Type startStateType = this.subBasco.TransitionCache.TransitionPool.Keys.FirstOrDefault();
            List<Type> stateTypes = this.subBasco.TransitionCache.TransitionPool.Keys.ToList();
            var states = this.Basco.StatesCache.RetrieveStates(stateTypes);
            this.subBasco.Initialize(states, startStateType);
            this.Basco.AddHierchary(this.subBasco);
            var baseState = this.Basco.StatesCache.RetrieveBaseState(this.compositeState.BaseStateType);
            this.compositeState.Initialize(this.subBasco, baseState);

            return this;
        }

        public ICompositeStateConfigurator<TTransitionTrigger> OnEnterAlwaysStartWithInitialState()
        {
            this.subBasco.BascoExecutor.AlwaysStartWithInitialState = true;
            return this;
        }
    }
}