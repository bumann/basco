namespace Basco.Sample.CompositeStates.Driver
{
    using Basco.Sample.CompositeStates.Driver.States;

    /// <summary>
    /// Example of state injection (IStateB).
    /// If the states are bound in named scope they may be injected just where needed.
    /// </summary>
    internal class Driver : IDriver
    {
        public const string DriverScope = "DriverScope";

        private readonly IStateB stateB;

        public Driver(IBasco<TransitionTrigger> basco, IStateB stateB)
        {
            this.Basco = basco;
            this.Basco.InitializeWithStartState<StateA>();
            this.stateB = stateB;
        }

        public IBasco<TransitionTrigger> Basco { get; private set; }

        public IStateB StateB
        {
            get { return this.stateB; }
        }
    }
}