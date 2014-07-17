namespace Basco.Sample.CompositeStates.Driver.States
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class StateB : IStateB
    {
        private CancellationTokenSource cancellationTokenSource;

        public event EventHandler ProcessingChanged;

        public int ItemCount { get; private set; }

        public void Execute()
        {
        }

        public void Enter()
        {
            this.cancellationTokenSource = new CancellationTokenSource();
            var token = this.cancellationTokenSource.Token;
            Task.Run(
                () =>
                {
                    while (!token.IsCancellationRequested)
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