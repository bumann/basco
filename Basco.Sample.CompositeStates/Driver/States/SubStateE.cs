namespace Basco.Sample.CompositeStates.Driver.States
{
    using System.Threading;

    public class SubStateE : ISubStateE
    {
        public IBasco<TransitionTrigger> Basco { get; set; }

        public void Execute()
        {
            Thread.Sleep(800);
            if (!this.Basco.IsRunning)
            {
                return;
            }
            
            this.Basco.Trigger(TransitionTrigger.ContinueD);
        }
    }
}