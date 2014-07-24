namespace Basco.Sample.CompositeStates.Driver.States
{
    using System.Threading;

    public class SubStateD : ISubStateD
    {
        private readonly IBasco<TransitionTrigger> basco;

        public SubStateD(IBasco<TransitionTrigger> basco)
        {
            this.basco = basco;
        }

        public void Execute()
        {
            Thread.Sleep(800);
            if (this.basco.IsRunning)
            {
                this.basco.Trigger(TransitionTrigger.GotoE);
            }
        }
    }
}