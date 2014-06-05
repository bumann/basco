namespace Basco.Sample.Basic.Fsm.States
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProcessingState : IProcessingState
    {
        private CancellationTokenSource cancellationTokenSource;

        public event EventHandler ProcessingChanged;

        public int ItemCount { get; private set; }

        public void Execute()
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            var token = this.cancellationTokenSource.Token;
            Task.Run(
                () =>
                {
                    while (!token.IsCancellationRequested && this.ItemCount < 80)
                    {
                        this.ItemCount++;
                        this.RaiseStateChanged();
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                },
                this.cancellationTokenSource.Token);
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