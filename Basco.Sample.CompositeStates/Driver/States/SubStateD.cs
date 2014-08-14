namespace Basco.Sample.CompositeStates.Driver.States
{
    using System.Threading;

    public class SubStateD : ISubStateD
    {
        public IBasco<TransitionTrigger> Basco { get; set; }

        public void Execute()
        {
            Thread.Sleep(800);
            if (!this.Basco.IsRunning)
            {
                return;
            }

            this.Basco.Trigger(TransitionTrigger.ContinueE);
        }
    }
}