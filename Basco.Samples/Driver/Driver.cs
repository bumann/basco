namespace Basco.Samples.Driver
{
    using Basco.Samples.Driver.States;

    internal class Driver : IDriver
    {
        public Driver(IBasco<TransitionTrigger> basco)
        {
            this.Basco = basco;
        }

        public IBasco<TransitionTrigger> Basco { get; private set; }
    }
}