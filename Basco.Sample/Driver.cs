namespace Basco.Samples
{
    using Basco.Samples.States;

    public class Driver : IDriver
    {
        private readonly IBasco<Transitions> basco;
        private readonly IConnectedState connectedState;

        public Driver(IBasco<Transitions> basco, IConnectedState connectedState)
        {
            this.basco = basco;
            this.connectedState = connectedState;
        }

        public void Connect()
        {
            this.basco.Start(this.connectedState);
        }

        public void Disconnect()
        {
            this.basco.Stop();
        }

        public void RunProgram()
        {
            this.basco.Trigger(Transitions.Run);
        }

        public void ProduceError()
        {
            this.basco.Trigger(Transitions.Error);
        }

        public void ResetError()
        {
            this.basco.Trigger(Transitions.Reset);
        }
    }
}