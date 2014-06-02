namespace Basco
{
    using System;

    internal class StateTransitionsBuilder<TTransitionTrigger> : IStateTransitionsBuilder<TTransitionTrigger>
        where TTransitionTrigger : IComparable
    {
        private readonly IStateTransitions<TTransitionTrigger> stateTransitions;

        public StateTransitionsBuilder(Type stateType, IStateTransitions<TTransitionTrigger> stateTransitions)
        {
            this.StateType = stateType;
            this.stateTransitions = stateTransitions;
        }

        public Type StateType { get; private set; }

        public TTransitionTrigger CurrentTrigger { get; set; }

        public IStateTransitions<TTransitionTrigger> StateTransitions
        {
            get { return this.stateTransitions; }
        }

        public void AddTransition()
        {
            this.SetTransition(this.StateType);
        }

        public void AddTransition<TState>() 
            where TState : class, IState
        {
            this.SetTransition(typeof(TState));
        }

        private void SetTransition(Type destinationStateType)
        {
            this.StateTransitions.Add(this.CurrentTrigger, destinationStateType);
        }
    }
}