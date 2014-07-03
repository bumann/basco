namespace Basco.Sample.UsingNinject.Driver
{
    using Basco;
    using Basco.Sample.UsingNinject.Driver.States;

    /// <summary>
    /// Example of state injection (IProcessingState).
    /// If the states are bound in named scope they may be injected just where needed.
    /// </summary>
    internal class Driver : IDriver
    {
        public const string DriverScope = "DriverScope";

        private readonly IProcessingState processingState;

        public Driver(IBasco<TransitionTrigger> basco, IProcessingState processingState)
        {
            this.Basco = basco;
            this.Basco.Initialize();
            this.processingState = processingState;
        }

        public IBasco<TransitionTrigger> Basco { get; private set; }

        public IProcessingState ProcessingState
        {
            get { return this.processingState; }
        }
    }
}