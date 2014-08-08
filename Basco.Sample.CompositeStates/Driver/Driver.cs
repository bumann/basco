namespace Basco.Sample.CompositeStates.Driver
{
    using System.Collections.Generic;
    using System.Linq;
    using Basco.Sample.CompositeStates.Driver.States;

    /// <summary>
    /// Example of state injection (IStateB).
    /// If the states are bound in named scope they may be injected just where needed.
    /// </summary>
    internal class Driver : IDriver
    {
        public const string DriverScope = "DriverScope";

        private readonly IStateB stateB;
        private readonly ISubStateD stateD;
        private readonly ISubStateE stateE;

        public Driver(IBasco<TransitionTrigger> basco, IEnumerable<IState> states, IStateB stateB)
        {
            this.Basco = basco;
            var allStates = states.ToList();
            this.Basco.Initialize(allStates, allStates.SingleOrDefault(x => x is IStateA));
            this.stateB = stateB;
            this.stateD = allStates.SingleOrDefault(x => x is ISubStateD) as ISubStateD;
            this.stateE = allStates.SingleOrDefault(x => x is ISubStateE) as ISubStateE;
        }

        public IBasco<TransitionTrigger> Basco { get; private set; }

        public IStateB StateB
        {
            get { return this.stateB; }
        }

        public bool SubStateDIsActive 
        {
            get
            {
                return this.stateD.Active;
            }
        }

        public bool SubStateEIsActive
        {
            get
            {
                return this.stateE.Active;
            }
        }
    }
}