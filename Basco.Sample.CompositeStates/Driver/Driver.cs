﻿namespace Basco.Sample.CompositeStates.Driver
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

        public Driver(IBasco<TransitionTrigger> basco, IEnumerable<IState> states, IStateB stateB)
        {
            this.Basco = basco;
            var allStates = states.ToList();
            this.Basco.Initialize(allStates, typeof(StateA));
            this.stateB = stateB;
        }

        public IBasco<TransitionTrigger> Basco { get; private set; }

        public IStateB StateB
        {
            get { return this.stateB; }
        }
    }
}