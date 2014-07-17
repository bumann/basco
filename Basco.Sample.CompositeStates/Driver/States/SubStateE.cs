namespace Basco.Sample.CompositeStates.Driver.States
{
    using System.Threading;
    using Scyano.Core;

    public class SubStateE : ISubStateE
    {
        private readonly IScyanoTaskExecutor taskExecutor;
        private readonly IBasco<TransitionTrigger> basco;

        public SubStateE(IScyanoTaskExecutor taskExecutor, IBasco<TransitionTrigger> basco)
        {
            this.taskExecutor = taskExecutor;
            this.basco = basco;
        }

        public bool IsInitialized { get; set; }

        public void Initialize()
        {
            this.taskExecutor.Initialize(
                () =>
                {
                    Thread.Sleep(1000);
                    this.basco.Trigger(TransitionTrigger.GotoD);
                });
        }

        public void Enter()
        {
            if (!this.IsInitialized)
            {
                this.Initialize();
                this.IsInitialized = true;
            }

            this.taskExecutor.StartOrResume();
        }

        public void Exit()
        {
            this.taskExecutor.Suspend();
        }

        public void Execute()
        {
        }
    }
}