namespace Basco.Sample.CompositeStates.Driver.States
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class StateB : IStateB
    {
        private readonly IBasco<TransitionTrigger> basco;
        private CancellationTokenSource cancellationTokenSource;

        public StateB(IBasco<TransitionTrigger> basco)
        {
            this.basco = basco;
        }

        public event EventHandler ProcessingChanged;

        public int ItemCount { get; private set; }

        public void Enter()
        {
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        public void Execute()
        {
            var token = this.cancellationTokenSource.Token;
            Task.Run(
               () =>
               {
                   while (!token.IsCancellationRequested && this.basco.IsRunning)
                   {
                       this.ItemCount++;
                       this.RaiseStateChanged();
                       Thread.Sleep(TimeSpan.FromSeconds(1));
                   }
               },
               token);
        }

        public void Exit()
        {
            this.cancellationTokenSource.Cancel();
        }

        private void RaiseStateChanged()
        {
            if (this.ProcessingChanged != null)
            {
                this.ProcessingChanged(this, new EventArgs());
            }
        }
    }
}