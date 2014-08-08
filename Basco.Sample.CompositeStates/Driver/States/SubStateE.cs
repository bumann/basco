namespace Basco.Sample.CompositeStates.Driver.States
{
    using System.Threading;

    public class SubStateE : ISubStateE
    {
        public IBasco<TransitionTrigger> Basco { get; set; }

        public bool Active { get; private set; }

        public void Execute()
        {
            this.Active = true;
            Thread.Sleep(800);
            if (!this.Basco.IsRunning)
            {
                return;
            }
            
            this.Basco.Trigger(TransitionTrigger.GotoD);
            this.Active = false;
        }
    }
}