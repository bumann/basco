namespace Basco.Samples.States
{
    using System;

    public interface IProcessingState : IState<Transitions>, IStateExit
    {
        event EventHandler ProcessingChanged;

        int ItemCount { get; }
    }
}