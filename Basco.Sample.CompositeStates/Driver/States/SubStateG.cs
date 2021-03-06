﻿namespace Basco.Sample.CompositeStates.Driver.States
{
    using System.Threading;

    public class SubStateG : ISubStateG
    {
        public IBasco<TransitionTrigger> Basco { get; set; }

        public void Execute()
        {
            Thread.Sleep(1000);
            if (!this.Basco.IsRunning)
            {
                return;
            }

            this.Basco.Trigger(TransitionTrigger.ContinueF);
        }
    }
}