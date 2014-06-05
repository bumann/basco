namespace Basco.Samples.Driver
{
    using Basco.Samples.Driver.States;

    /// <summary>
    /// Example of state injection (IProcessingState).
    /// If the states are bound in named scope they may be injected just where needed.
    /// </summary>
    internal class Driver : IDriver
    {
        private readonly IProcessingState processingState;

        public Driver(IBasco<TransitionTrigger> basco, IProcessingState processingState)
        {
            this.Basco = basco;
            this.processingState = processingState;
        }

        public IBasco<TransitionTrigger> Basco { get; private set; }

        public IProcessingState ProcessingState
        {
            get { return this.processingState; }
        }
    }
}