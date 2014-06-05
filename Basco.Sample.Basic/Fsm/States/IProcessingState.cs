namespace Basco.Sample.Basic.Fsm.States
{
    using System;

    public interface IProcessingState : IState, IStateExit
    {
        event EventHandler ProcessingChanged;

        int ItemCount { get; }
    }
}