namespace Basco.Sample.Basic.Fsm.States
{
    using System;

    public interface IProcessingState : IStateExit, IStateUsingBasco<TransitionTrigger>
    {
        event EventHandler ProcessingChanged;

        int ItemCount { get; }
    }
}